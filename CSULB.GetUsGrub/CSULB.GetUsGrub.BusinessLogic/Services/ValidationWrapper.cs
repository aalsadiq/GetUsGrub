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
    public class ValidationWrapper<T>
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

        public ResponseDto<T> ExecuteValidator()
        {
            var validationResult = _validator.Validate(_data, _ruleSet);
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<T>
                {
                    Data = _data,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
            return new ResponseDto<T>()
            {
                Data = _data
            };
        }

    }
}