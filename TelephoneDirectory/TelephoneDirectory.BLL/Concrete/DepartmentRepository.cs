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
    public class DepartmentRepository : BaseRepository<Department, int, TelephoneDirectoryEntities>, IDepartmentRepository
    {
        readonly TelephoneDirectoryEntities _context;

        public DepartmentRepository(TelephoneDirectoryEntities context) : base(context)
        {
            _context = context;
        }
        public AppReturn Delete(int id)
        {
            try
            {
                Department department = _context.Departments.Find(id);
                department.IsActive = false;
                return AppReturn.Successful("Department deleted successfully.");
            }
            catch (Exception)
            {
                // TODO : Logger has to add here.
                return AppReturn.InvalidOperation("An unexpected error has occured while deleting department.");
            }
        }
    }
}
