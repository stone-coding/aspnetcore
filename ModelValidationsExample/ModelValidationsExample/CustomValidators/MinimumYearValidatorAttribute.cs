using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.CustomValidators
{
    public class MinimumYearValidatorAttribute: ValidationAttribute
    {

        // default MinimumYear 
        public int MinimumYear { get; set; } = 2010;
        // {0} is 2005 in MinimumYearValidator(2005)
        public string DefaultErrorMessage { get; set; } = "Year should not be less than {0}";

        public MinimumYearValidatorAttribute() { 
        
        }

        // create a constructor to receive the minimumYear after MinimumYearValidator to here
        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear = minimumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
                if(value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year >= MinimumYear)
                {
                    // pass the Minimum year after MinimumYearValidator to here 


                    //ErrorMessage ?? DefaultErrorMessage, MinimumYear equivalent to below 
                    /*  if (DefaultErrorMessage != null)
                    {
                        ErrorMessage = DefaultErrorMessage;
                    }else
                    {
                        ErrorMessage = Convert.ToString(MinimumYear);
                    */
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
            
        }
            

        }

    }

