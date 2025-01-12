using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApp.Data
{
    public class DataContext<T> where T : class
    {
        private readonly List<T> _list = [];

        public IEnumerable<T> Items() => _list;

        public bool Save(T item)
        {
            _list.Add(item);

            return true;
        }
    }
}