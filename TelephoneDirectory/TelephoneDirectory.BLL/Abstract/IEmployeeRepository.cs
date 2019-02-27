using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Core;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.BLL.Abstract
{
    public interface IEmployeeRepository : IBaseRepository<Employee, int, TelephoneDirectoryEntities>
    {
        Employee BringByEmail(string email);
        Employee BringByTelephone(string telephone);
        AppReturn Delete(int id);
    }
}
