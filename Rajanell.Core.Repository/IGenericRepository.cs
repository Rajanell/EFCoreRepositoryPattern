using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Core.Repository
{
    public interface IGenericRepository<T> : ICommandRepository<T>, IQueryRepository<T>, IPagedQueryRepository<T> where T : class
    {
    }
}
