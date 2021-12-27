using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Core.Repository
{
    public interface ICommandRepository<T>
    {
        Task Add(T item);
        Task Add(IEnumerable<T> items);
        void Update(T item);
        void Update(IEnumerable<T> items);
        Task Save();
    }
}
