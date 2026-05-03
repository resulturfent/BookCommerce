using AutoMapper;
using BLL.AbstractService;
using BLL.Dtos;
using Book_Commerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Register()
        {
            //login aşaması yapılacak
            return View();
            //pull request için 

        }
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register");
            }

            var checkUsername = await _userService.CheckUsername(userViewModel.Username);

            if (userViewModel.Email == null)
            {
                ViewBag.MailHata = "Email boş bırakılamaz.";
            }
            if (userViewModel.Username == null)
            {
                ViewBag.UsernameHata = "Kullanıcı adı boş bırakılamaz";
            }
            if (checkUsername)
            {
                ViewBag.SameUsernameHata = "Bu kullanıcı adı var. Başka bir tane kullanınız.";
            }
            if (userViewModel.Password == null)
            {
                ViewBag.PasswordHata = "Parola boş bırakılamaz.";
            }
            else
            {
                if (userViewModel.Password.Length <= 6)
                {
                    ViewBag.PasswordMoreThanHata = "Parola en az 6 karakter olmalıdır.";
                }

                if (!userViewModel.Password.Any(char.IsUpper))
                {
                    ViewBag.PasswordBigWordHata = "Parola en az 1 büyük harf içermeli!";
                }

                if (!userViewModel.Password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    ViewBag.PasswordCharacterHata = "Parola en az bir özel karakter içermelidir.";
                }

            }
            var userDto = _mapper.Map<UserDto>(userViewModel);
            await _userService.Register(userDto);

            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var userDto = await _userService.Login(username, password);

            if (userDto != null)
            {
                var userViewModel = _mapper.Map<UserViewModel>(userDto);
                // Session bilgileri eklemek.
                HttpContext.Session.SetInt32("UserId", userViewModel.Id);
                HttpContext.Session.SetString("Username", userViewModel.Username);
                HttpContext.Session.SetString("IsAdmin", userViewModel.IsAdmin.ToString());
                return RedirectToAction("Index", "Home", userViewModel);
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre yanlış";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId"); // Giriş yapan kullanıcı bilgisine ulaştık.
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var user = await _userService.GetUserById(userId.Value);
                return View(_mapper.Map<UserViewModel>(user));
            }
        }
    }
}
