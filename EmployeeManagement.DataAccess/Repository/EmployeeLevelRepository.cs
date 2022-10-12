using EmployeeManagement.DataAccess.Repository.IRepository;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repository
{
    public class EmployeeLevelRepository : Repository<EmployeeLevel>, IEmployeeLevelRepository
    {
        private ApplicationDbContext _db;

        public EmployeeLevelRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(EmployeeLevel obj)
        {
            _db.Levels.Update(obj);
        }
    }
}
