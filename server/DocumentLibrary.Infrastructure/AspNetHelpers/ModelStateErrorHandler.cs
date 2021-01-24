using System.Collections.Generic;
using System.Linq;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentLibrary.Infrastructure.AspNetHelpers
{
    public class ModelStateErrorHandler : IModelStateErrorHandler
    {
        public List<string> GetErrors(ModelStateDictionary modelState)
            => modelState.Values
                .SelectMany(msv => msv.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
    }
}