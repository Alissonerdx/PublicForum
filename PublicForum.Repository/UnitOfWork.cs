using PublicForum.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public EFContext _db;

        public UnitOfWork(EFContext db)
        {
            _db = db;
        }
        public void BeginTransaction()
        {
            if (_db == null)
                throw new System.ArgumentException("Context not initialized");
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
