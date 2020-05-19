using AfrroStock.Data;
using AfrroStock.Enums;
using AfrroStock.Models;
using AfrroStock.Models.DTOs;
using AfrroStock.Models.ViewModels;
using AfrroStock.Repository.Extension;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudyMATEUpload.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfrroStock.Repository.Generic
{
    public class UserManager : ModelManager<ApplicationUser>
    {
        private readonly IMapper _mapper;
        private readonly AuthRepository _auth;

        public UserManager()
        {

        }
        public UserManager(ApplicationDbContext context, IMapper mapper, AuthRepository auth) : base(context)
        {
            _mapper = mapper;
            _auth = auth;
        }
        public async ValueTask<(string, ApplicationUser, string)> RegisterUser(RegisterViewModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                bool hasAccount = await Item().AnyAsync(u => u.Email.ToLower() == model.Email.ToLower());
                if (hasAccount) { return (null, null, "user already exists"); }

                string passwordHash = Hash.GetHashedValue(model.Password);
                ApplicationUser user = model.Convert<RegisterViewModel, ApplicationUser>(_mapper);
                user.PasswordHash = passwordHash;
                try
                {
                    await Add(user);
                }
                catch (Exception ex)
                {
                    return (null, null, ex.Message);
                }
                return (await LoginUser(new LoginViewModel { Email = model.Email, Password = model.Password }));
            }
            return (null, null, "password must match");

        }

        public async ValueTask<(string, ApplicationUser, string)> LoginUser(LoginViewModel model)
        {
            var loginPassword = model.Password;
            var user = await Item().Where(x => x.Email.ToLower() == model.Email.ToLower())
                                                .FirstOrDefaultAsync();
            if (user == null) { return (null, null, "no such user in the database"); }

            string incomingHash = Hash.GetHashedValue(loginPassword);
            if (incomingHash != user.PasswordHash)
            {
                return (null, null, "password do not match");
            }
            return (_auth.GetToken(model.Email, user.Role), user, null);
        }

        public async ValueTask<(bool, ApplicationUser, string)> ChangePassword(PatchKnownPasswordDTO patch)
        {
            ApplicationUser user = await FindOne(u => u.Id == patch.Id);
            if (user != null)
            {
                string passwordHash = Hash.GetHashedValue(patch.OldPassword);
                if (passwordHash == user.PasswordHash)
                {
                    string newHashedPassword = Hash.GetHashedValue(patch.NewPassword);
                    user.PasswordHash = newHashedPassword;
                    return await Update(user);
                }
            }
            return (false, null, "user not found");
        }

        public async ValueTask<(bool, ApplicationUser, string)> ForgotPassword(PatchUnknownPasswordDTO patch, string code)
        {
            ApplicationUser user = await FindOne(u => u.Email.ToLower() == patch.Email.ToLower());
            if (user != null)
            {
                if (code == user.Code && user.CodeIssued <= user.CodeWillExpire)
                {

                }
                if (patch.NewPassword == patch.ConfirmNewPassword)
                {
                    string passwordHash = Hash.GetHashedValue(patch.NewPassword);
                    user.PasswordHash = passwordHash;
                    return await Update(user);
                }

            }
            return (false, null, "user not found"); ;
        }


    }
}
