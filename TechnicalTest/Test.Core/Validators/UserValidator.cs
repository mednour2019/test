using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Core.DTOs;

namespace Test.Core.Validators
{
    public class UserValidator :AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                  .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Status).Must(status => status == 0 || status == 1)
.WithMessage("\"Status must be either 0 (inactive) or 1 (active).");
        }
    }
}
