using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AfrroStock.Models;
using AfrroStock.Models.ViewModels;
using AfrroStock.Repository;
using AfrroStock.Repository.Extension;
using AfrroStock.Repository.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AfrroStock.Models.DTOs;
using AfrroStock.Services;
using Microsoft.AspNetCore.JsonPatch;
//using StudyMATEUpload.Services;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager _acc;
        private readonly IMapper _mapper;
        private readonly IEmailSender _email;
        
        public AccountController(UserManager account, 
            IEmailSender email,
            IMapper mapper)
        {
            _acc = account;
            _mapper = mapper;
           _email = email;
           
        }
        [HttpPost("login")]
        public async ValueTask<IActionResult> Post([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (token, whoLoggedIn, error) = await _acc.LoginUser(model);
                if(token != null)
                {
                    UserViewModel dto = whoLoggedIn.Convert<ApplicationUser, UserViewModel>(_mapper);
                    dto.Token = token;
                    return Ok(dto);
                }
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Post([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (token, whoRegistered, error) = await _acc.RegisterUser(model);
                if (token != null)
                {
                    UserViewModel dto = whoRegistered.Convert<ApplicationUser, UserViewModel>(_mapper);
                    dto.Token = token;
                    return Ok(dto);
                }
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [Authorize]
        [HttpGet("getcurrentuser")]
        public async ValueTask<IActionResult> GetCurrentUser()
        {
            var userEmail = HttpContext.User.Claims
                                                .Where(u => u.Type.Contains("emailaddress"))
                                                .Select(u => u.Value).FirstOrDefault();
            UserViewModel user = await _acc.Item()
                                            .Where(u => u.Email.ToLower() == userEmail.ToLower())
                                            .Select(u => u.Convert<ApplicationUser, UserViewModel>(_mapper))
                                            .FirstOrDefaultAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpPut("user")]
        public async ValueTask<IActionResult> Put([FromBody] UserDTO model)
        {
            if (ModelState.IsValid)
            {
                bool isUser = await _acc.Item().AnyAsync(u => model.Id == u.Id);
                if (isUser)
                {
                    ApplicationUser userMappedFromModel = model.Convert<UserDTO, ApplicationUser>(_mapper);
                    (bool succeeded, ApplicationUser updatedUser, string error) = await _acc.Update(userMappedFromModel);
                    if (succeeded) return Ok(updatedUser.Convert<ApplicationUser, UserDTO>(_mapper));
                    return BadRequest(new { Message = error });
                }
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [Authorize]
        [HttpPatch("user/{id:int}")]
        public async ValueTask<IActionResult> Put([FromBody]JsonPatchDocument<ApplicationUser> patchDoc, int id)
        {
            var model = await _acc.Item().FindAsync(id);
            if (model != null)
            {
                patchDoc.ApplyTo(model, ModelState);
                if (ModelState.IsValid)
                {
                    (bool succeeded, ApplicationUser updatedUser, string error) = await _acc.Update(model);
                    if (succeeded) return Ok(_mapper.Map<ApplicationUser, UserViewModel>(updatedUser));
                    return BadRequest(new { Message = error });
                }
                return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
            }
            return BadRequest(new { Message = "No such item" });
        }

        [Authorize]
        [HttpPut("changepassword")]
        public async ValueTask<IActionResult> Put([FromBody] PatchKnownPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, user, error) = await _acc.ChangePassword(model);
                if (succeeded) return Ok(new { succeeded });
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });

        }

        [HttpPost("generatecode")]
        public async ValueTask<IActionResult> Generate(string email, bool validate = false)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _acc.Item()
                                                    .Where(u => u.Email.ToLower() == email.ToLower())
                                                    .FirstOrDefaultAsync();
                if (user != null)
                {
                    string code = $"{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}";
                    user.Code = code;
                    user.CodeIssued = DateTime.Now;
                    user.CodeWillExpire = DateTime.Now.AddDays(2);
                    (bool succeeded, ApplicationUser _, string error) = await _acc.Update(user);
                    if (succeeded)
                    {
                        string subject = validate ? "Validate Email" : "Change Password";
                        string message = validate ? BuildEmailVerificationyText(user.FirstName, code) : BuildPasswordRecoveryText(user.FirstName, code);
                        await _email.SendEmailAsync(email, subject, message.ToString());
                        return NoContent();
                    }
                }

                return NotFound();
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });

        }

        

        [HttpPut("forgotpassword")]
        public async ValueTask<IActionResult> Put(string code, [FromBody] PatchUnknownPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _acc.Item().Where(u => u.Email.Equals(model.Email)).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (code == user.Code && user.CodeIssued <= user.CodeWillExpire)
                    {
                        var (succeeded, _, error) = await _acc.ForgotPassword(model, code);
                        if (succeeded) return Ok(new { succeeded });
                        return BadRequest(new { Message = error });
                    }
                    return BadRequest(new { Message = "code is invalid or has expired" });
                }
                return BadRequest(new { Message = "user not found" });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });

        }

        [HttpPut("verifyemail")]
        public async ValueTask<IActionResult> Put(string code)
        {
            if (ModelState.IsValid)
            {
                var user = await _acc.Item().Where(u => u.Code == code).FirstOrDefaultAsync();
                if (user != null)
                {
                    if(user.CodeIssued <= user.CodeWillExpire)
                    {
                        user.VerifiedOn = DateTime.Now;
                        user.IsVerified = true;
                        var (succeeded, _, error) = await _acc.Update(user);
                        if (succeeded) return Ok(new { succeeded });
                        return BadRequest(new { Message = error });
                    }
                    return BadRequest(new { Message = "code is invalid or has expired" });
                }
                return BadRequest(new { Message = "user not found" });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });

        }

        [HttpGet]
        public async ValueTask<IActionResult> Get()
        {
            ICollection<UserViewModel> users = await _acc.Item()
                                                        .Select(u => u.Convert<ApplicationUser, UserViewModel>(_mapper))
                                                        .ToListAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            UserViewModel user = await _acc.Item()
                                          .Where(u => id == u.Id)
                                          .Select(u => u.Convert<ApplicationUser, UserViewModel>(_mapper))
                                          .FirstOrDefaultAsync();
            return Ok(user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            var model = new ApplicationUser { Id = id };
            (bool succeeded, string error) = await _acc.Delete(model);
            if (succeeded) return NoContent();
            return NotFound(new { Message = error });
        }

        private string BuildPasswordRecoveryText(string firstName, string code)
        {
            StringBuilder message = new StringBuilder(100);
            message.Append($"Dear {firstName},<br/><br/>");
            message.Append($"You requested a change in password.<br/>Kindly use this link to reset your password.<br/>The code will expire in less than two (2) days.<br/><br/>");
            message.Append($"<a href='{Request.Scheme}://{Request.Host}/forgotpassword?code={code}'>Reset Password</a><br/><br/>");
            message.Append("Thank you.<br/><br/>Sincerely,<br/>Admin.");
            return message.ToString();
        }

        private string BuildEmailVerificationyText(string firstName, string code)
        {
            StringBuilder message = new StringBuilder(100);
            message.Append($"Dear {firstName},<br/><br/>");
            message.Append($"Please verify your email.<br/>The code will expire in less than two (2) days.<br/>");
            message.Append($"<a href='{Request.Scheme}://{Request.Host}/verifyemail?code={code}'>Verify Email</a><br/><br/>");
            message.Append("Thank you.<br/><br/>Sincerely,<br/>Admin.");
            return message.ToString();
        }
    }
}