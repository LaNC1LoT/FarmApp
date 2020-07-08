using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Core.Enums;
using FarmApp.Infrastructure.Data.Contexts;
using FarmAppServer.BusinessModels;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
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

        private bool ValidateAutorization(Request<UserAutorization> request, out IList<ModelState> modelStates)
        {
            modelStates = new List<ModelState>();
            if (request?.Data?.Login?.Length < 4 || request?.Data?.Login?.Length > 20)
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.Login),
                    PropertyError = "Введите от 4 до 20 символов"
                });
            }
            if (request.Data.Password.Length < 6 || request.Data.Password.Length > 20)
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.Password),
                    PropertyError = "Введите от 6 до 20 символов"
                });
            }
            return modelStates.Any();
        }

        public Task<Response<UserToken>> GetToken(Request<UserAutorization> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserToken>
                {
                    Data = new UserToken()
                };

                if (ValidateAutorization(request, out IList<ModelState> modelStates))
                {
                    response.Result = Result.NotValid;
                    response.Title = "Авторизация";
                    response.Message = "Неверные данные";
                    response.Data.Validations = modelStates;
                    return response;
                };

                var user = context.Users.FirstOrDefault(x => x.Login == request.Data.Login && x.Password == request.Data.Password);
                if (user == null)
                {
                    response.Result = Result.Error;
                    response.Title = "Авторизация";
                    response.Message = "Неверный логин или пароль";
                }
                else if (user.IsDeleted == true)
                {
                    response.Result = Result.Error;
                    response.Title = "Авторизация";
                    response.Message = "Пользователь заблокирован";
                }
                else
                {
                    response.Result = Result.Success;
                    response.Title = "Авторизация";
                    response.Message = "Успешно";
                    response.Data.UserLogin = new UserLogin
                    {
                        Token = "Token",
                        Login = user.Login,
                        UserName = user.UserName,
                        RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == user.RoleTypeId).Description
                    };
                }

                return response;
            });
        }

        public enum Crud
        {
            Create,
            Update,
            Delete
        }

        public Task<Response<UserCrud>> CreateUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserCrud>();
                var user = GetEntity(request.Data);

                if (ValidateCrud(request, out IList<ModelState> modelStates))
                {
                    response.Result = Result.NotValid;
                    response.Title = "Добавление";
                    response.Message = "Неверные данные";
                    response.Data.Validations = modelStates;
                }
                else
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    response.Result = Result.Success;
                    response.Title = "Добавление";
                    response.Message = "Успешно";
                    response.Data.User = GetData(user);
                }
                return response;
            });
        }

        public Task<Response<UserCrud>> UpdateUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserCrud>();

               
                if (ValidateCrud(request, out IList<ModelState> modelStates))
                {
                    response.Result = Result.NotValid;
                    response.Title = "Редактирование";
                    response.Message = "Неверные данные";
                    response.Data.Validations = modelStates;
                }
                else
                {
                    var user = GetEntity(request.Data);
                    context.Users.Update(user);
                    context.SaveChanges();

                    response.Result = Result.Success;
                    response.Title = "Редактирование";
                    response.Message = "Успешно";
                    response.Data.User = GetData(user);
                }
                return response;
            });
        }

        public Task<Response<UserData>> DeleteUser(Request<UserData> request)
        {
            return Task.Run(() =>
            {
                var response = new Response<UserData>();
                var user = context.Users.FirstOrDefault(x => x.Id == request.Data.Id);
                if (user == null)
                {
                    response.Result = Result.NotFound;
                    response.Title = "Удаление";
                    response.Message = "Пользователь не найден";
                }
                else
                {
                    user.IsDeleted = true;
                    context.Users.Update(user);
                    context.SaveChanges();
                    response.Result = Result.Success;
                    response.Title = "Удаление";
                    response.Message = "Пользователь удален";
                }
                return response;
            });
        }

        private bool ValidateCrud(Request<UserData> request, out IList<ModelState> modelStates)
        {
            modelStates = new List<ModelState>();
            if (request?.Data?.Login?.Length < 4 || request?.Data?.Login?.Length > 20)
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.Login),
                    PropertyError = "Введите от 4 до 20 символов"
                });
            }
            if (request?.Data?.Password?.Length < 6 || request?.Data?.Password?.Length > 20)
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.Password),
                    PropertyError = "Введите от 6 до 20 символов"
                });
            }
            if (request?.Data?.UserName?.Length < 6 || request?.Data?.UserName?.Length > 100)
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.UserName),
                    PropertyError = "Введите от 6 до 100 символов"
                });
            }
            if (context.Users.Any(x => x.Login == request.Data.Login))
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.Login),
                    PropertyError = "Login занят"
                });
            }
            if (!context.RoleTypes.Any(x => x.Id == request.Data.RoleTypeId))
            {
                modelStates.Add(new ModelState
                {
                    PropertyName = nameof(request.Data.RoleTypeId),
                    PropertyError = "Не найден тип пользователя"
                });
            }

            return modelStates.Any();
        }

        private User GetEntity(UserData user)
        {
            return new User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                UserName = user.UserName,
                RoleTypeId = user.RoleTypeId,
                IsDeleted = user.IsDeleted
            };
        }

        private UserData GetData(User user)
        {
            return new UserData
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Login,
                UserName = user.UserName,
                RoleTypeId = user.RoleTypeId,
                //RoleName = context.RoleTypes.FirstOrDefault(x => x.Id == user.RoleTypeId)?.Description,
                IsDeleted = user.IsDeleted
            };
        }
    }
}
