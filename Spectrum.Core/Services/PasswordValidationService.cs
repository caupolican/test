using System;
using System.Text.RegularExpressions;

namespace Spectrum.Services
{

    public interface IPasswordValidationService {

        (bool Result, string Message) Validate(string password);
    }

    public class PasswordValidationService: IPasswordValidationService
    {
        /// <summary>
        //  Validation Rules
        //  1. String must consist of a mixture of letters and numerical digits only, with at least one of each.
        //  2. String must be between 5 and 12 characters in length.
        //  3. String must not contain any sequence of characters immediately followed by the same sequence
        /// </summary>
        /// <returns></returns>
        public (bool Result, string Message) Validate(string password)
        {

            if (password.Length < 5 || password.Length > 12)
            {
                return (false, @"Must be between 5 and 12 characters in length.");
            }

            if (!Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$"))
            {
                return (false, "Must consist of a mixture of letters and numerical digits only, with at least one of each.");
            }

            if (Regex.IsMatch(password, @"(...+)\1"))
            {
                return (false, "Must not contain any sequence of characters immediately followed by the same sequence.");
            }

            return (true, "OK");
        }
    }
}
