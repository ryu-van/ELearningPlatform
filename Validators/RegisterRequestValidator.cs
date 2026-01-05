using FluentValidation;
using E_learning_platform.DTOs.Requests;

namespace E_learning_platform.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(100).WithMessage("Họ tên không được quá 100 ký tự");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^\d{10,11}$").WithMessage("Số điện thoại phải có 10-11 chữ số");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự")
                .Matches(@"[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất 1 chữ hoa")
                .Matches(@"[a-z]").WithMessage("Mật khẩu phải chứa ít nhất 1 chữ thường")
                .Matches(@"[0-9]").WithMessage("Mật khẩu phải chứa ít nhất 1 số")
                .Matches(@"[\!\?\*\.]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt (!?*.)");
        }
    }
}
