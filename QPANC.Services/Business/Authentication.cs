using Microsoft.AspNetCore.Identity;
using QPANC.Domain;
using QPANC.Services.Abstract;
using QPANC.Services.Abstract.Entities.Identity;
using QPANC.Services.Abstract.I18n;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QPANC.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        private readonly QpancContext _context;
        private readonly RT.Comb.ICombProvider _comb;
        private readonly ILoggedUser _loggedUser;
        private readonly IMessages _messages;

        public Authentication(
            UserManager<User> userManager,
            QpancContext context,
            RT.Comb.ICombProvider comb,
            ILoggedUser loggedUser,
            IMessages messages)
        {
            this._userManager = userManager;
            this._context = context;
            this._comb = comb;
            this._loggedUser = loggedUser;
            this._messages = messages;
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest login)
        {
            var incorrectPasswordOrUsername = new BaseResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Errors = new Dictionary<string, string>
                {
                    { nameof(login.Password), this._messages.ErrorMessage_IncorrectPasswordOrUsername }
                }
            };

            var user = await this._userManager.FindByNameAsync(login.UserName);
            if (user == default)
            {
                return incorrectPasswordOrUsername;
            }

            var isAuthenticated = await this._userManager.CheckPasswordAsync(user, login.Password);
            if (!isAuthenticated)
            {
                return incorrectPasswordOrUsername;
            }

            var expiresAt = DateTimeOffset.Now.AddYears(1);
            var sessionId = this._comb.Create();
            var session = new Session
            {
                SessionId = sessionId,
                UserId = user.Id,
                ExpireAt = expiresAt
            };
            this._context.Sessions.Add(session);
            await this._context.SaveChangesAsync();

            return new BaseResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = new LoginResponse
                {
                    SessionId = session.SessionId,
                    UserId = session.UserId,
                    UserName = user.UserName,
                    ExpiresAt = session.ExpireAt
                }
            };
        }

        public async Task<BaseResponse> Logout()
        {
            var session = await this._context.Sessions.FindAsync(this._loggedUser.SessionId);
            if (session == default)
            {
                return new BaseResponse(statusCode: HttpStatusCode.NotFound);
            }
            this._context.Sessions.Remove(session);
            await this._context.SaveChangesAsync();
            return new BaseResponse(statusCode: HttpStatusCode.OK);
        }

        public async Task<BaseResponse> Register(RegisterRequest register)
        {
            var userNameAlreadyTaken = new BaseResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Errors = new Dictionary<string, string>
                {
                    { nameof(register.UserName), this._messages.ErrorMessage_UserNameAlreadyTaken }
                }
            };

            var passwordTooWeak = new BaseResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Errors = new Dictionary<string, string>
                {
                    { nameof(register.Password), this._messages.ErrorMessage_PasswordTooWeak }
                }
            };

            var user = await this._userManager.FindByNameAsync(register.UserName);
            if (user != default)
            {
                return userNameAlreadyTaken;
            }

            user = new User()
            {
                UserName = register.UserName,
                FirstName = register.FirstName,
                LastName = register.LastName
            };
            var result = await this._userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                return new BaseResponse(statusCode: HttpStatusCode.OK);
            }
            else
            {
                return passwordTooWeak;
            }
        }
    }
}
