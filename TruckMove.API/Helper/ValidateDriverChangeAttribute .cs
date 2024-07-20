using Microsoft.AspNetCore.Mvc;

namespace TruckMove.API.Helper
{
    public class ValidateDriverChangeAttribute : TypeFilterAttribute
    {
        public ValidateDriverChangeAttribute() : base(typeof(ValidateDriverChangeAttributeFilter))
        {
        }
    }
}
