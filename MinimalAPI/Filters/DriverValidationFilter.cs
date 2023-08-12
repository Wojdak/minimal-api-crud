using FluentValidation;
using MinimalAPI.Models;

namespace MinimalAPI.Filters
{
    public class DriverValidationFilter : IEndpointFilter
    {
        private readonly IValidator<Driver> _validator;

        public DriverValidationFilter(IValidator<Driver> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //find the Driver to validate
            var driver = context.Arguments.FirstOrDefault(d => d.GetType() == typeof(Driver)) as Driver;

            //validate with fluentvalidation
            var result = await _validator.ValidateAsync(driver);

            //return errors if found
            if (!result.IsValid)
                return Results.Json(result.Errors, statusCode: 400);

            return await next(context);
        }
    }
}
