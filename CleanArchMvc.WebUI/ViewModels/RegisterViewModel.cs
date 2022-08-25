﻿using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.WebUI.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "The {0} msut be at least {2} and at max {1} characteres long.", MinimumLength = 10)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, ErrorMessage = "The {0} msut be at least {2} and at max {1} characteres long.", MinimumLength = 10)]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string? ConfirmPassword { get; set; }
}
