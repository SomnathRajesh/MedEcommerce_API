using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_Core.DTO;
using MedEcommerce_DB;

namespace MedEcommerce_Core
{
    public interface IUserService
    {
        Task<AuthenticatedUser>SignUp(User user);
        Task<AuthenticatedUser> SignIn(User user);
    }
}
