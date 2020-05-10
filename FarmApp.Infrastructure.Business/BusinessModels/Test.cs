using FarmApp.Domain.Core.Entity;
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
