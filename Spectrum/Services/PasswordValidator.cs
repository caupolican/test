using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using Spectrum;

namespace Spectrum.Core.Validators
{
    public class PasswordValidator
    {
        public ValidationResult Validate(string password)
        {
            Contract.Ensures(Contract.Result<ValidationResult>() != null);
            var validationResult = new ValidationResult();

            //Business Rules
            //1. String must consist of a mixture of letters and numerical digits only, with at least one of each.
            //2. String must be between 5 and 12 characters in length.
            //3. String must not contain any sequence of characters immediately followed by the same sequence

            if (password.Length < 5 || password.Length > 12)
            {
                validationResult.IsValid = false;
                validationResult.ValidationMessages.Add("Must be between 5 and 12 characters in length.");
            }

            if (!Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$"))
            {
                validationResult.IsValid = false;
                validationResult.ValidationMessages.Add("Must consist of a mixture of letters and numerical digits only, with at least one of each.");
            }

            if (Regex.IsMatch(password, @"(...+)\1"))
            {
                validationResult.IsValid = false;
                validationResult.ValidationMessages.Add("Must not contain any sequence of characters immediately followed by the same sequence.");
            }

            return validationResult;
        }
    }
}