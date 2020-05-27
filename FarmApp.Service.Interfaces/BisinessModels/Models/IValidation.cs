using System;
using System.Collections.Generic;
using System.Text;

namespace FarmApp.Service.Interfaces.BisinessModels.Models
{
    public interface IValidation
    {
        string IsValid { get; set; }
        string Property { get; set; }
        string Message { get; set; }
    }
}
