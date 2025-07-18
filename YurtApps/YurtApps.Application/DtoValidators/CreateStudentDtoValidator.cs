using FluentValidation;
using YurtApps.Application.DTOs.StudentDTOs;

namespace YurtApps.Application.DtoValidators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator() 
        {
            RuleFor(x => x.StudentPhoneNumber)
                .Matches(@"^05\d{9}$").WithMessage("The phone number must be in the format ‘05XXXXXXXXX’.");

            RuleFor(x => x.StudentEmail)
                .EmailAddress().WithMessage("Enter a valid e-mail address.");
        }
    
    }
}