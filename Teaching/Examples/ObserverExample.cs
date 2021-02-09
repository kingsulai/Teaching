using System;
using System.Collections.Generic;
using System.Text;

namespace Teaching.Examples
{

    public interface IObservable
    {

        void AddObserver(Action<IObservable> observable);

        void RemoveObserver(Action<IObservable> observable);

    }


    public class D
    {
        public D()
        {
            var x = new ObservableList<int>();
            x.Add(1);
            x.AddObserver((i) => Console.WriteLine(i));
            x.Add(2); //soll 2 Ausgeben weil Observer aufgerufen wird
            x.Set(2, 3); //soll 3 Ausgeben weil Observer aufgerufen wird

        }
    }


    /// <summary>
    /// Ziel ist es dass hier Werte vom Typ T gespeichert werden.
    /// Immer wenn eines der Objekte geändert sollen alle Observer Funktionen aufgerufen werden.
    /// Es sollen auch Observer aufgerufen Werden wenn eines der Objecte hinzugefügt oder entfernt wird.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableList<T> : IObservable
    {

        public void AddObserver(Action<IObservable> observable)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void RemoveObserver(Action<IObservable> observable)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Add(T t)
        {
            //TODO
            throw new NotImplementedException();
        }
        public void Remove(T t)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hier können wir nicht sagen ob es geändert wurde
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T Get(int index)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Set(int index, T t)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Anz Elemente
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            //TODO
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            var s = "";
            for (int i=0;i<Count();i++)
            {
                s += Get(i)+" ";
            }
            return s;
        }
    }
}
