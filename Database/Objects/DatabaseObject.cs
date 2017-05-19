using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Objects
{
    public interface DatabaseObject<T>
    {
        void CopyFrom(T other);
        object[] Key { get; }
    }
}
