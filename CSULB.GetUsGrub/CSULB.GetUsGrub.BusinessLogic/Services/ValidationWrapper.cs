using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// A <c>ValidationWrapper</c> class.
    /// Contains methods for calling validators to validate an object and wraps it in a ResponseDto.
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @update: 03/15/2018
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationWrapper<T> : IValidationWrapper
    {
        private readonly T _data;
        private readonly AbstractValidator<T> _validator;
        private readonly string _ruleSet;

        public ValidationWrapper(T data, string ruleSet, AbstractValidator<T> validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = ruleSet;
        }

        // TODO: @Jenn How to pass in the validator when you have to instantiate it --> Instantiate it in the Strategy [-Jenn]
        public ValidationWrapper(T data, AbstractValidator<T> validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = "";
        }

        public ResponseDto<bool> ExecuteValidator()
        {
            var validationResult = _validator.Validate(_data, ruleSet: _ruleSet);
            if (!validationResult.IsValid && validationResult.Errors != null)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
            if (!validationResult.IsValid)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = "Something went wrong. Please try again later."
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

    }
}
