using CXChange.Modules.Kyc.Application.Validator;
using FluentValidation;

namespace CXChange.Modules.Kyc.Application.Commands.Kyc
{
    public class InitiateKycValidator : AbstractValidator<InitiateKyc>
    {
        public InitiateKycValidator()
        {

            RuleFor(x => x.Image).KycPhoto();
            RuleFor(x => x.NationalId).NationalId();
            //RuleFor(x => x.Name).FullName();
            //RuleFor(x => x.SurName).FullName();


        }
    }
}
