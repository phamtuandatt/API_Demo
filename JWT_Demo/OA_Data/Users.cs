using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA_Data
{
    [Table("User")]
    public class Users
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Hoten { get; set; }

        public string Email { get; set; }

    }
}