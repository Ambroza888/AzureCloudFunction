using Microsoft.AspNetCore.Http;
using Valence.Models;

namespace Valence.Validation
{
    public class ValidateQueryRequest
    {
        public static LocationParams Validate(IQueryCollection query)
        {
            var QueryParam = new LocationParams()
            {
                Location = query["location"],
                Categories = query["categories"]
            };

            if (QueryParam.Categories != null && QueryParam.Location != null) {
                return QueryParam;
            } else {
                return null;
            }
        }
    }
}