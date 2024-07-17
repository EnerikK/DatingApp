using DatingApp.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var apiError = new ErrorResponse();

            if(!context.ModelState.IsValid)
            {
                apiError.StatusCode = 1;
                apiError.StatusMessage = "Bad Request";
                apiError.TimeStamp = DateTime.Now;
                
                var errors = context.ModelState.AsEnumerable();

                foreach(var error in errors)
                {
                    foreach(var modelError in error.Value.Errors)
                    {
                        apiError.Errors.Add(modelError.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }

    }
}
