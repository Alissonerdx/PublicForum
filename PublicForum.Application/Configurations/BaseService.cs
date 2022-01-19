using PublicForum.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.Configurations
{
    public class BaseService
    {
        private IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        internal void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }
        internal int Commit()
        {
            return _unitOfWork.Commit();
        }
        internal async Task<int> CommitAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}
