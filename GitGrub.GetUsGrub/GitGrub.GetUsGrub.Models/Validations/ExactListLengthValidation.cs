using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models.Validations
{
    class ExactListLengthValidation : ValidationAttribute
    {
        private readonly int _validLength;

        public ExactListLengthValidation(int length)
        {
            _validLength = length;
        }
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count == _validLength;
            }

            return false;
        }
    }
}
