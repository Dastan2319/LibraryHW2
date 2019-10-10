using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DLL;
using DLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class UsersService : IUsersService
    {
        IUnitOfWork db { get; set; }

        public UsersService(IUnitOfWork uow)
        {
            db = uow;
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public UsersDTO GetUsers(UsersDTO id)
        {
            if (id != null)
            {
                var users = db.Users.GetAll().Where(x => x.FIO == id.FIO && x.password == id.password).FirstOrDefault();
                
                if (users!= null)
                {
                    return new UsersDTO { FIO = "true"};
                }
                else
                {
                    return new UsersDTO();
                }
            }
            else
            {
                return new UsersDTO();
            }
        }

        public IEnumerable<UsersDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UsersDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Users>, List<UsersDTO>>(db.Users.GetAll());
        }

        public void MakeUsers(UsersDTO orderDto)
        {
            Users user = new Users
            {
                FIO = orderDto.FIO,
                password=orderDto.password
            };
            db.Users.Create(user);
            db.Save();
        }

        public void SaveUpdate(Users orderDto)
        {
            Users users = new Users
            {
                FIO = orderDto.FIO,
                password = orderDto.password
            };
            db.Users.Update(users);
            db.Save();
        }
    }
}
