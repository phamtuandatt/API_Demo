using OA_Data;

namespace OA_Service
{
    public interface IUserService
    {
        // Provide method get User - Insert - Update
        IEnumerable<Users> GetUsers();
        Users GetUser(long id);
        Users CheckLogin(string username, string password);
        void InsertUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(long id);
    }
}