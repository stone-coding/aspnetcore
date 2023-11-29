using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationsExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person:IValidatableObject
    {
        [Required(ErrorMessage ="{0} name can not be empty or null")] // mark personName is required field
        [Display(Name ="Person Name")]
        // maximum length 40 and minimum length is 3 
        [StringLength(40, MinimumLength =3,ErrorMessage ="{0} should be {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z.]*$",ErrorMessage ="{0} should be contains only alphabets, space , and dot(.)")]
        public string? personName { get; set; }

        [EmailAddress(ErrorMessage = "{0} should be contains only alphabets, space , and dot(.)")]
        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Email { get; set; }

        [Phone(ErrorMessage ="{0} should contain 10 digits")]
        public string? Phone { get; set; }

        [Required(ErrorMessage ="{0} can't be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} do not match")]
        [Display(Name ="Re-enter Password")]
        public string? ConfirmPassword { get; set; }
        // $ signs means print as it is 
        [Range(0,999.99, ErrorMessage ="{0} should be between ${1} and ${2}")]
        public double? Price { get; set; }



        /*[MinimumYearValidator(2005,ErrorMessage ="Date of birth should not be newer than Jan 01, {0}")]*/
        [MinimumYearValidator(2005)]
        // Bind never will omit the property in model 
        //[BindNever]
        public DateTime? DateOfBirth { get; set; }

        
        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "From Date should be older than or equal to To date")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }
        
        public List<string?> Tags { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"Person Object - Person name:{personName}, " +
                $"Email: {Email}, Phone: {Phone}, Password: {Password}, " +
                $"ConfirmPassword: {ConfirmPassword}, Price:{Convert.ToString(Price)}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateOfBirth.HasValue == false && Age.HasValue == false)
            {
                // yield allows to return one more result
                // new[]{nameof(Age)} is a string type easy to maintain when change to other name
                yield return new ValidationResult("Either Date of Birth" +
                    " or Age must be applied", new[] {nameof(Age)});
            }
        }
    }
}
