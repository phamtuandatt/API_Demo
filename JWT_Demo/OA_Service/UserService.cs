using OA_Data;
using OA_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service
{
    public class UserService : IUserService
    {
        private readonly IRepo<Users> _repo;

        public UserService(IRepo<Users> repo)
        {
            _repo = repo;
        }

        public Users CheckLogin(string username, string password)
        {
            return _repo.CheckLogin(x => x.UserName == username && x.Password == password);
        }

        public void DeleteUser(long id)
        {
            _repo.Delete(GetUser(id));
        }

        public Users GetUser(long id)
        {
            return _repo.Get(id);
        }

        public IEnumerable<Users> GetUsers()
        {
            return _repo.GetAll();
        }

        public void InsertUser(Users user)
        {
            _repo.Insert(user);
        }

        public void UpdateUser(Users user)
        {
            _repo.Update(user);
        }
    }
}
