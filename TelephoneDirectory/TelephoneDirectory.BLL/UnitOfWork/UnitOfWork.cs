using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.BLL.Abstract;
using TelephoneDirectory.BLL.Concrete;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TelephoneDirectoryEntities _context;
        private IEmployeeRepository employeeRepository;
        private IDepartmentRepository departmentRepository;
        private ITitleRepository titleRepository;

        public UnitOfWork(TelephoneDirectoryEntities context)
        {
            _context = context;
        }

        public IEmployeeRepository EmployeeRepository => employeeRepository ?? (employeeRepository = new EmployeeRepository(_context));
        public IDepartmentRepository DepartmentRepository => departmentRepository ?? (departmentRepository = new DepartmentRepository(_context));
        public ITitleRepository TitleRepository => titleRepository ?? (titleRepository = new TitleRepository(_context));

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true; 
            }
            catch (Exception e)
            {
                // Logging must add here.
                return false;
            }
        }
    }
}
