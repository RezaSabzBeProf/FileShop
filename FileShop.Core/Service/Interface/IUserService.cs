using AspCore_Course.Models;
using AspCore_Course.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShop.Core.Service.Interface
{
    public interface IUserService
    {
        User GetUserByUserName(string userName);

        bool Login(LoginViewModel model);

        void AddUser(User user);
    }
}
