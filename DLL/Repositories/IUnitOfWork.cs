using DLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repositories;

namespace DLL.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Authors> Authors { get; }
        IRepository<Books> Books { get; }
        IRepository<Ganre> Ganre { get; }
        IRepository<Users> Users { get; }
        IRepository<Message> Message { get; }
        void Save();
    }
}
