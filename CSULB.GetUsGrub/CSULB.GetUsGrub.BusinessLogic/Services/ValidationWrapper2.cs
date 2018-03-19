using CSULB.GetUsGrub.Models;
using FluentValidation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class ValidationWrapper2<T>
    {
        private readonly T _data;
        private readonly IValidator<T> _validator;
        private readonly string _ruleSet;

        public ValidationWrapper2(T data, IValidator<T> validator, string ruleSet)
        {
            _data = data;
            _validator = validator;
            _ruleSet = ruleSet;
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
                Data = _data,
                Error = null
            };
        }
    }
}
