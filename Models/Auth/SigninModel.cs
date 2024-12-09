using System.ComponentModel.DataAnnotations;

public class SigninModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Password cannot exceed 100 characters")]
    public string Password { get; set; } = string.Empty;
}
