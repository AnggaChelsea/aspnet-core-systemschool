using FluentValidation;
using NetAngularAuthWebApi.Models.Domain;

namespace NetAngularAuthWebApi.helpers.Validate{
    public class ValidateRegisterService : AbstractValidator<RegistrationTam>{
        public void RegisterTamValidator(){
            RuleFor(registrationTam => registrationTam.Email).NotNull();
        }
    }
}