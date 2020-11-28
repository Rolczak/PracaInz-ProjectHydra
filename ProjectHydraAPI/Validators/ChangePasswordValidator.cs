using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePassModel>
    {
        private readonly HydraDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ChangePasswordValidator(HydraDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            RuleFor(cp => cp).MustAsync((x, cancellation) => IsPasswordCorrect(x)).WithMessage("Hasło niepoprawne.");
            RuleFor(cp => cp.NewPassword).NotEmpty().MaximumLength(7);
        }

        private async Task<bool> IsPasswordCorrect(ChangePassModel changePassModel)
        {
            var userDb = _db.Users.Where(u => u.Id == changePassModel.UserID).FirstOrDefault();
            if(userDb == null)
            {
                return false;
            }
            var isCorrect = await _userManager.CheckPasswordAsync(userDb, changePassModel.OldPassword);
            return isCorrect;
        }
    }
}
