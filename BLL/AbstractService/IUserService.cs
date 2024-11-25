using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AbstractService
{
    public interface IUserService
    {
        Task Register(UserDto userDto);
        Task<UserDto> Login(string username, string password);
        Task<bool> CheckUsername(string username);
        Task<UserDto> GetUserById(int id);
    }
}
