using FarmApp.Domain.Core.Entities;
using FarmApp.Service.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Infrastructure.Business.BusinessModels
{
    public class Test : ITest
    {
        public User User { get; set; }
    }
}
