using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{

    public interface IObservable<T>
    {

        void AddObserver(Action<T> observer);

        void RemoveObserver(Action<T> observer);

    }


    /// <summary>
    /// Ziel ist es dass hier Werte vom Typ T gespeichert werden.
    /// Immer wenn eines der Objekte geändert sollen alle Observer Funktionen aufgerufen werden.
    /// Es sollen auch Observer aufgerufen Werden wenn eines der Objecte hinzugefügt oder entfernt wird.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableList<T> : IObservable<(int, T)>
    {
        private Action<(int, T)> observers;
        private readonly List<T> elements = new List<T>();


        public static ObservableList<T> operator +(ObservableList<T> a, List<T> b)
        {
            foreach (var e in b)
                a.Add(e);
            return a;
        }

        /// <summary>
        /// Example operator overloading
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Observable<T> : IObservable<T>
        {
            private Action<T> observers;
            private T value;

            /// <summary>
            /// Constructor will initialize the value when there are still no observers
            /// </summary>
            /// <param name="value"></param>
            public Observable(T value)
            {
                this.value = value;
            }

            /// <summary>
            /// Get or set the value of the Observer. Setting it will call the observers
            /// </summary>
            public T Value
            {
                get { return value; }
                set
                {
                    this.value = value;
                    if (observers != null)
                        observers(value);
                }
            }

            /// <summary>
            /// Add an observer
            /// </summary>
            /// <param name="observer">the observer to add</param>
            public void AddObserver(Action<T> observer)
            {
                observers += observer;
            }

            /// <summary>
            /// Remove an observer
            /// </summary>
            /// <param name="observer">the Observer to remove</param>
            public void RemoveObserver(Action<T> observer)
            {
                observers -= observer;
            }
        }
        /// <summary>
        /// Add a new observer.
        /// </summary>
        /// <param name="observer"> the observer to add</param>
        public void AddObserver(Action<(int, T)> observer)
        {
            observers += observer;
        }
        /// <summary>
        /// Remove an observer
        /// </summary>
        /// <param name="observer"> the observer to remove</param>
        public void RemoveObserver(Action<(int, T)> observer)
        {
            observers -= observer;
        }

        /// <summary>
        /// Add a new element to the list
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            elements.Add(t);
            if (observers != null)
                observers((elements.Count - 1, t));
        }

        /// <summary>
        /// Remove the value and call the observer
        /// </summary>
        /// <param name="t"></param>
        public void Remove(T t)
        {
            var index = elements.IndexOf(t);
            elements.Remove(t);
            if (observers != null)
                observers((index, t));
        }

        /// <summary>
        /// Returns an observer for the value witch in turb will call the observer of the list with its index
        /// </summary>
        /// <param name="index">index of the value</param>
        /// <returns></returns>
        public Observable<T> Get(int index)
        {

            var o = new Observable<T>(elements[index]);
            if (observers != null)
                o.AddObserver(o =>
                {
                    elements[index] = o;
                    observers((index, elements[index]));
                });
            return o;
        }

        /// <summary>
        /// Set the element at the given index. This notifies all observers;
        /// </summary>
        /// <param name="index"> must be in range [0,ObservableList.Count).</param>
        /// <param name="value"></param>
        public void Set(int index, T value)
        {
            elements[index] = value;

            if (observers != null)
                observers((index, value));
        }

        /// <summary>
        /// Anz Elemente
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return elements.Count;
        }

        /// <summary>
        /// Simply print all the elements of the list
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(",", elements);
        }

    }
}
