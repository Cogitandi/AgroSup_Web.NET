using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Description = "Hasło musi zawierać cyfrę" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Description = "Hasło musi zawierać co najmniej jedną wielką literę [A-Z]" };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Description = "Hasło musi zawierać co najmniej jeden znak specjalny [@,!,#,...]" };
        }
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Description = "Nieprawidłowy email" };
        }
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Description = "Nieprawidłowe dane" };
        }
        public override IdentityError InvalidToken()
        {
            return new IdentityError { Description = "Nieprawidłowe dane" };
        }
    }
}
