using AuthServer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.NickName).NotEmpty().Length(6, 20);
            RuleFor(user => user.Password).NotEmpty().Length(8, 20);
        }
    }
}
