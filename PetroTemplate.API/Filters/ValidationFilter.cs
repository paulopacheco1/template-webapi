using Microsoft.AspNetCore.Mvc.Filters;
using PetroTemplate.Domain.Exceptions;

namespace PetroTemplate.API.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var fieldErrors = context.ModelState
                .Where(ms => ms.Value?.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Select(ms => new FieldValidationErrors(ms.Key, ms.Value!.Errors.Select(e => e.ErrorMessage)));

            throw new RequestValidationException(fieldErrors);
        }
    }
}
