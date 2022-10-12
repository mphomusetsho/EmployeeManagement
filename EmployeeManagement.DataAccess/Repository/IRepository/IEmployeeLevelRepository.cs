using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repository.IRepository
{
    public interface IEmployeeLevelRepository : IRepository<EmployeeLevel>
    {
        void Update(EmployeeLevel obj);
    }
}
