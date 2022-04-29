using AspCore_Course;
using AspCore_Course.Models;
using AspCore_Course.Models.DTOs;
using FileShop.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShop.Core.Service
{
    public class UserService : IUserService
    {
        FarsLearnContext _context;
        public UserService(FarsLearnContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(p => p.UserName == userName);
        }

        public bool Login(LoginViewModel model)
        {
            bool login = _context.Users.Any(p => p.UserName == model.UserName && p.Password == Password_helper.EncodePassword(model.Password));
            return login;
        }
    }
}
