using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        int Commit();
        Task<int> CommitAsync();
    }
}
