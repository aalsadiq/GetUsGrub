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
    /// @update: 04/04/2018
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationWrapper<T> : IValidationWrapper
    {
        // Declaration of class variables
        private readonly T _data;
        private readonly AbstractValidator<T> _validator;
        private readonly string _ruleSet;

        // Constructors
        public ValidationWrapper(T data, string ruleSet, AbstractValidator<T> validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = ruleSet;
        }
        public ValidationWrapper(T data, AbstractValidator<T> validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = "";
        }

        /// <summary>
        /// The ExecuteValidator method.
        /// This method executes the validator specified.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>A ResponseDto which contains data that is of type bool and possibly an error message.</returns>
        public ResponseDto<bool> ExecuteValidator()
        {
            // Execute the validator
            var validationResult = _validator.Validate(_data, ruleSet: _ruleSet);

            // If validation is false and the validation contains its own error messages
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
            // If validation is not valid, then return a ResponseDto with data set to false and a general error message
            if (!validationResult.IsValid)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // If validation passes, then return a ResponseDto with data set to true
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

    }
}
