using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Util.Contracts
{
    interface IPooledCollection<T>
    {
        T Get();

        void Add(T item);

    }
}
