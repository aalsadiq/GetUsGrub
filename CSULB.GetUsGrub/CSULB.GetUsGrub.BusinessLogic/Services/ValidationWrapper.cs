using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// A <c>ValidationWrapper</c> class.
    /// Contains methods for calling validators to validate an object and wraps it in a ResponseDto.
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @update: 03/15/2018
    /// </summary>
    /// <typeparam name="TA"></typeparam>
    /// <typeparam name="TB"></typeparam>
    public class ValidationWrapper<TA, TB>
    {
        private readonly TA _data;
        private readonly TB _validator;
        private readonly string _ruleSet;

        public ValidationWrapper(TA data, string ruleSet, TB validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = ruleSet;
        }

        // TODO: @Jenn How to pass in the validator when you have to instantiate it --> Instantiate it in the Strategy [-Jenn]
        public ValidationWrapper(TA data, TB validator)
        {
            _data = data;
            _validator = validator;
            _ruleSet = "";
        }

        public ResponseDto<TA> ExecuteValidator()
        {
            var validationResult = _validator.Validate(_data, _ruleSet);
            if (!validationResult.IsValid)
            {
                var errorsList = new List<string>();
                validationResult.Errors.ToList().ForEach(e => errorsList.Add(e.ErrorMessage));
                var errors = JsonConvert.SerializeObject(errorsList);
                return new ResponseDto<TA>
                {
                    Data = _data,
                    Error = JsonConvert.SerializeObject(errors)
                };
            }
            return new ResponseDto<TA>()
            {
                Data = _data
            };
        }

    }
}