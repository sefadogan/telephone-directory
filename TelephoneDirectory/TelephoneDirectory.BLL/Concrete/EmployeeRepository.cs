using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.BLL.Abstract;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.BLL.Concrete
{
    public class EmployeeRepository : BaseRepository<Employee, int, TelephoneDirectoryEntities>, IEmployeeRepository
    {
        readonly TelephoneDirectoryEntities _context;

        public EmployeeRepository(TelephoneDirectoryEntities context) : base(context)
        {
            _context = context;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(x => x.Email == email);
        }
    }
}
