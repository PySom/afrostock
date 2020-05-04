using AfrroStock.Data;
using AfrroStock.Enums;
using AfrroStock.Models;
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
        public async ValueTask<(string, string)> RegisterUser(RegisterViewModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                bool hasAccount = await Item().AnyAsync(u => u.Email.ToLower() == model.Email.ToLower());
                if (hasAccount) { return (null, "user already exists"); }

                string passwordHash = Hash.GetHashedValue(model.Password);
                ApplicationUser user = model.Convert<RegisterViewModel, ApplicationUser>(_mapper);
                user.PasswordHash = passwordHash;
                try
                {
                    await Add(user);
                }
                catch (Exception ex)
                {
                    return (null, ex.Message);
                }
                return (await LoginUser(new LoginViewModel { Email = model.Email, Password = model.Password }));
            }
            return (null, "password must match");

        }

        public async ValueTask<(string, string)> LoginUser(LoginViewModel model)
        {
            var loginPassword = model.Password;
            var user = await Item().Where(x => x.Email.ToLower() == model.Email.ToLower())
                                                .FirstOrDefaultAsync();
            if (user == null) { return (null, "no such user in the database"); }

            string incomingHash = Hash.GetHashedValue(loginPassword);
            if (incomingHash != user.PasswordHash)
            {
                return (null, "password do not match");
            }
            return (_auth.GetToken(model.Email, user.Role), null);
        }
        

    }
}
