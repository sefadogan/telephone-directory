using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.Core;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.BLL.Abstract
{
    public interface IDepartmentRepository : IBaseRepository<Department, int, TelephoneDirectoryEntities>
    {
        AppReturn Delete(int id);
        Department BringByName(string name);
    }
}
