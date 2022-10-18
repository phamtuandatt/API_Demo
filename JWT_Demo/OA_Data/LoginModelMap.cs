using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data
{
    public class LoginModelMap
    {
        public LoginModelMap(EntityTypeBuilder<LoginModel> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.UserName).IsRequired();
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.RefreshToken);
            entityBuilder.Property(t => t.RefreshTokenExpiryTime);
        }
    }
}
