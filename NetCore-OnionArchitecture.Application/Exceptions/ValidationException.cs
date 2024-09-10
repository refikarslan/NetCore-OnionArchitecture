using FluentValidation.Results;

namespace NetCore_OnionArchitecture.Application.Exceptions
{
    public class ValidationException : Exception 
    {
        public ValidationException() : base("Bir veya daha fazla doğrulama hatası üretildi ")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }   
        
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach(var failure in failures) 
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}