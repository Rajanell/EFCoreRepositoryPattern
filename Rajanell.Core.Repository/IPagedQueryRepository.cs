using Rajanell.Core.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rajanell.Core.Repository
{
    public interface IPagedQueryRepository<T>
    {
        Task<PagedList<T>> GetAll(ResourceParameters resourceParameters);
        Task<PagedList<T>> Find(Expression<Func<T, bool>> query, ResourceParameters resourceParameters);
    }
}
