using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder.NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 chanracters")
                    .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
                    .Matches("[a-z]").WithMessage("Password must contain 1 lowercase letter")
                    .Matches("[0-9]").WithMessage("Password must contain 1 number letter")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain 1 non alphanumeric character");

            return options;

        }
    }
}
