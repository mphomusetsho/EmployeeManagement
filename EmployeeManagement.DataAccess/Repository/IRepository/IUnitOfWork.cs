using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
