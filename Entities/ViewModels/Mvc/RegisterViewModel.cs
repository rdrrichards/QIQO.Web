﻿using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
