using System.Threading.Tasks;
using TireDrift.Data;
using TireDrift.Models;
using TireDrift.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace TireDrift.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TiresDbContext _tiresDbContext;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, TiresDbContext tiresDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tiresDbContext = tiresDbContext;
        }

        public async Task<SignInResult> LogIn(UserLoginViewModel logInRequest)
        {
            var user = await _userManager.FindByNameAsync(logInRequest.UserName);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, logInRequest.Password);

                if (passwordCheck)
                {
                    return await _signInManager.PasswordSignInAsync(user, logInRequest.Password, false, false);
                }
            }
            return SignInResult.Failed;
        }
        public User Get(string id)
        {
            return _tiresDbContext.Users.Include(x => x.HotelTires).FirstOrDefault(x => x.Id == id);
        }



        public async Task<IdentityResult> Register(UserRegisterViewModel registerRequest)
        {
            User user = new User()
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                PhoneNumber = registerRequest.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return result;
            }
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteAsync(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public List<User> GetSearchedAsync(string firstName)
        {
            var users = _tiresDbContext.Users.ToList();
            if (!firstName.IsNullOrEmpty())
            {
                users = users.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower())).ToList();
            }

            return users;
        }

        public async Task<List<object>> GetEmployeesAsync(string firstName)
        {
            var users = GetSearchedAsync(firstName);
            List<object> employees = new List<object>();
            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, "User"))
                {
                    var roleList = await _userManager.GetRolesAsync(user);
                    string role = "";
                    switch (roleList.FirstOrDefault())
                    {
                        case "Manager":
                            role = "Мениджър";
                            break;
                        case "Consultant":
                            role = "Консултант";

                            break;
                        case "Technician":
                            role = "Техник";
                            break;
                        case "Logistics":
                            role = "Логистичен персонал";
                            break;
                    }
                    var employee = new
                    {
                        user.Id,
                        user.UserName,
                        user.FirstName,
                        user.LastName,
                        user.PhoneNumber,
                        user.Email,
                        Role = role
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}