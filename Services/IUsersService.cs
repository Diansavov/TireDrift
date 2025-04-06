using TireDrift.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using TireDrift.Models;

namespace TireDrift.Services
{
   public interface IUserService
    {
        Task<SignInResult> LogIn(UserLoginViewModel logInRequest);
        Task<IdentityResult> Register(UserRegisterViewModel registerRequest);
        Task LogOut();
        Task DeleteAsync(string id);
        List<User> GetSearchedAsync(string firstName);
        Task<List<object>> GetEmployeesAsync(string firstName);
        User Get(string id);

        Task AddEmployee(EmployeeRegister employeeRegister);
        Task EditEmployee(EmployeeRegister employeeRegister);

    }
}