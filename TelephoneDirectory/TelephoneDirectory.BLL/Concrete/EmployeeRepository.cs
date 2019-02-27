using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.BLL.Abstract;
using TelephoneDirectory.Core;
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

        public Employee BringByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(x => x.Email == email);
        }
        public Employee BringByTelephone(string telephone)
        {
            return _context.Employees.FirstOrDefault(x => x.Telephone == telephone);
        }
        public AppReturn Delete(int id)
        {
            try
            {
                Employee employee = _context.Employees.Find(id);
                employee.IsActive = false;
                return AppReturn.Successful("Employee deleted successfully.");
            }
            catch (Exception)
            {
                // TODO : Logger has to add here.
                return AppReturn.InvalidOperation("An unexpected error has occured while deleting employee.");
            }
        }
    }
}
