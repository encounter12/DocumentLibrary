using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentLibrary.Infrastructure.AspNetHelpers.Contracts
{
    public interface IModelStateErrorHandler
    {
        List<string> GetErrors(ModelStateDictionary modelState);
    }
}