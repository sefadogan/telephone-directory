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

        public UnitOfWork(TelephoneDirectoryEntities context)
        {
            _context = context;
        }

        public IEmployeeRepository EmployeeRepository => employeeRepository ?? (employeeRepository = new EmployeeRepository(_context));
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
            catch (Exception)
            {
                // Logging must add here.
                return false;
            }
        }
    }
}
