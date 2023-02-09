﻿using BLL.Services;
using System.ComponentModel.DataAnnotations;

namespace BLL.CustomValidator
{
    public class UniqueEmail: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string? email = value.ToString();
                var users = Task.Run(() => new UserService().Get()).Result;
                if (users != null)
                {
                    if (users.Any(x => x.Email.ToLower() == email.ToLower()))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return null;
        }
    }
}
