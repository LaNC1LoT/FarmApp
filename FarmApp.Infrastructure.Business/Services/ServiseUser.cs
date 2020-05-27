using FarmApp.Domain.Core.Entities;
using FarmApp.Domain.Interfaces.UnitOfWorks;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FarmApp.Infrastructure.Business.Services
{
    public interface IServiceUser
    {

    }
    public class ServiseUser : IServiceUser
    {
        private readonly IUnitOfWork unitOfWork;
        public ServiseUser(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Invoke(Request<UserModel> request)
        {
           
            //var validationRequest = new Validation();
            //validationRequest.ValidationUser(request);
            //throw new NotImplementedException();
        }

        //public Task<Response<TData>> Invoke<TData>(Request<TData> request) where TData : IData
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class Validation
    {
        //internal void ValidationUser(IRequest<IUserDto> request)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
