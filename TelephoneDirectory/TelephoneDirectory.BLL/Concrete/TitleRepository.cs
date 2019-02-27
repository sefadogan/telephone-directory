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
    public class TitleRepository : BaseRepository<Title, int, TelephoneDirectoryEntities>, ITitleRepository
    {
        public TitleRepository(DbContext context) : base(context)
        {
        }
    }
}
