using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using BLL.StaticMethod;
using DAL.AbstractRepository;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(IRepository<User> userRepository, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<bool> CheckUsername(string username)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.Any(u => u.Username == username);
            return user;
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(x => x.Username == username && x.Password == password);
            //  var user = users.FirstOrDefault(x => x.Username == username && x.Password == SifreHash.SifreHashle(password));
            return _mapper.Map<UserDto>(user);
        }
        /*YUKARIDAKİ VE AŞAĞIDAKİ HASHLEMELERİ AÇARSAK KULLANICI EŞLEŞMESİ OLUYOR ANCAK ADMİN EŞLEŞMESİ OLMUYOR. ADMİN EŞLEŞMESİ OLMADIĞI İÇİNDE ŞİMDİLİK İNAKTİF DURUMA GETİRİLDİ.*/

        public async Task Register(UserDto userDto)
        {
          // userDto.Password = SifreHash.SifreHashle(userDto.Password);
            var user = _mapper.Map<User>(userDto);
            EmailRequestDto emailRequest = new()
            {
                To = userDto.Email,
                From = "tahirozden84@gmail.com",
                IsBodyHtml = true,
                Body = "<h1>Kayıt Başarılı</h1>",
                Subject = "Bilgilendirme"
            };
            await _emailService.SendEmailAsync(emailRequest);
            await _userRepository.AddAsync(user);
        }

       public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }
    }
}
