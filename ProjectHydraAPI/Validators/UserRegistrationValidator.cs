using FluentValidation;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Validators
{
    public class UserRegistrationValidator : AbstractValidator<RegistrationViewModel>
    {
        private readonly HydraDbContext _db;
        public UserRegistrationValidator(HydraDbContext db)
        {
            _db = db;

            RuleFor(user => user.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .Must(IsEmailUnique).WithMessage("Email zajęty.");
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(50);
            RuleFor(user => user.Password).NotEmpty().MinimumLength(7);


        }

        private bool IsEmailUnique(RegistrationViewModel registrationVM, string name)
        {
            var userDb = _db.Users.Where(u => u.NormalizedUserName == name.ToLower()).SingleOrDefault();
            if(userDb == null)
            {
                return true;
            }
            return false;
        }
    }
}
