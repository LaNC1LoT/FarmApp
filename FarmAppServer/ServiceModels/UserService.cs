using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Core.Enums;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.BusinessModels;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FarmAppServer.ServiceModels
{
    public class UserService
    {
        private readonly FarmAppContext context;
        public UserService(FarmAppContext context)
        {
            this.context = context;

        }

        public Task<Response<UserView>> GetDataFromView()
        {
            return Task.Run(() =>
            {
                return new Response<UserView>
                {
                    Result = Result.Success,
                    Data = new UserView
                    {
                        UserData = context.Users.Join(context.RoleTypes,
                            u => u.RoleTypeId, r => r.Id, (u, r) => new UserData
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                Login = u.Login,
                                Password = u.Password,
                                RoleTypeId = u.RoleTypeId,
                                RoleName = r.Description,
                                IsDeleted = u.IsDeleted
                            }),
                        UserFilter = context.RoleTypes.Where(w => w.IsDeleted == false)
                            .Select(s => new UserFilter
                            {
                                Id = s.Id,
                                RoleName = s.Description
                            })
                    }
                };
            });
        }

        public Task<Response<UserToken>> GetToken(Request<UserAutorization> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserToken>();
                var user = context.Users.FirstOrDefault(x => x.Login == request.Data.Login && x.Password == request.Data.Password && x.IsDeleted == false);
                if (user == null)
                {
                    response.Result = Result.NotFound;
                }
                else
                {
                    response.Result = Result.Success;
                    response.Data = new UserToken
                    {
                        Token = "TestToken",
                        UserLogin = new UserLogin
                        {
                            Login = user.Login,
                            UserName = user.UserName,
                            RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == user.RoleTypeId).Description
                        }
                    };
                }

                return response;
            });
        }

        public Task<Response<UserData>> CreateUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserData>();
                var user = new User
                {
                    Login = request.Data.Login,
                    Password = request.Data.Password,
                    UserName = request.Data.UserName,
                    RoleTypeId = request.Data.RoleTypeId,
                };

                try
                {
                    response.Data.RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == request.Data.RoleTypeId).Description;
                    context.Users.Add(user);
                    context.SaveChanges();

                    request.Data.Id = user.Id;
                    response.Result = Result.Success;
                    response.Data = request.Data;
                }
                catch
                {
                    response.Result = Result.UnknownError;
                }
                return response;
            });
        }

        public Task<Response<UserData>> UpdateUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserData>();
                var user = new User
                {
                    Id = request.Data.Id,
                    Login = request.Data.Login,
                    Password = request.Data.Password,
                    UserName = request.Data.UserName,
                    RoleTypeId = request.Data.RoleTypeId,
                };

                try
                {
                    response.Data.RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == request.Data.RoleTypeId).Description;
                    context.Users.Update(user);
                    context.SaveChanges();

                    response.Result = Result.Success;
                    response.Data = request.Data;
                }
                catch
                {
                    response.Result = Result.UnknownError;
                }
                return response;
            });
        }

        public Task<Response<UserData>> DeleteUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserData>();
                var user = new User
                {
                    Id = request.Data.Id,
                    Login = request.Data.Login,
                    Password = request.Data.Password,
                    UserName = request.Data.UserName,
                    RoleTypeId = request.Data.RoleTypeId,
                };

                try
                {
                    response.Data.RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == request.Data.RoleTypeId).Description;
                    context.Users.Remove(user);
                    context.SaveChanges();

                    response.Result = Result.Success;
                    response.Data = request.Data;
                }
                catch
                {
                    response.Result = Result.UnknownError;
                }
                return response;
            });
        }
    }
}
