using FluentValidation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XMEN.Core.DTOs;

namespace XMEN.Infrastructure.Validators
{
    public class DNAValidator : AbstractValidator<MutantRequest>
    {
        public DNAValidator()
        {
            RuleFor(DNA => DNA.DNA)
                .NotNull()
                .NotEmpty()
                .Must(ValidateInputs)
                .WithMessage("the DNA strand only supports the characters A,T,C and G");
        }


        private bool ValidateInputs(List<string> DNA)
        {
            foreach (string item in DNA)
            {
                if (!Regex.IsMatch(item, @"^[ATCG]+$"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
