using FluentValidation;
using MinimalAPI.Models;

namespace MinimalAPI.Filters
{
    public class UserValidationFilter : IEndpointFilter
    {
        private readonly IValidator<UserDto> _validator;

        public UserValidationFilter(IValidator<UserDto> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var user = context.Arguments.FirstOrDefault(x => x.GetType() == typeof(UserDto)) as UserDto;

            var result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
                return Results.Json(result.Errors, statusCode: 400);

            return await next(context);
        }
    }
}
