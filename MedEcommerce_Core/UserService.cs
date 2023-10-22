using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEcommerce_Core.CustomExceptions;
using MedEcommerce_Core.DTO;
using MedEcommerce_Core.Utilities;
using MedEcommerce_DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedEcommerce_Core
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(ApplicationDbContext context,IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail.Equals(user.UserEmail));
            if (checkUser != null)
            {
                throw new UseremailAlreadyExistsException("User Email Already Exists");
            }
            user.Password = _passwordHasher.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var role = _context.Roles.FirstOrDefault(r=>r.Id==user.RoleId).Role;
            var id = _context.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail).Id;

            return new AuthenticatedUser
            {
                Useremail = user.UserEmail,
                Token = JwtGenerator.GenerateUserToken(user.UserEmail),
                Role = role,
                Id = id
            };

        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u=>u.UserEmail == user.UserEmail);
            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser.Password,user.Password)==PasswordVerificationResult.Failed)
            {
                throw new InvalidUseremailPasswordException("Invalid user email or password");
            }
            var role = _context.Roles.FirstOrDefault(r=>r.Id == dbUser.RoleId).Role;
            var id = dbUser.Id;
            return new AuthenticatedUser
            {
                Useremail = user.UserEmail,
                Token = JwtGenerator.GenerateUserToken(user.UserEmail),
                Role = role,
                Id= id
            };
        }
    }
}
