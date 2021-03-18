using System.Collections.Generic;
using Valence.Dtos;
using Valence.Models;

namespace Valence.Helper
{
    public static class GenerateList
    {
        public static List<YelpToReturnDto> Generate(YelpParams param)
        {
            List<YelpToReturnDto> ListToReturn = new List<YelpToReturnDto>();

            for (int i = 0; i < param.businesses.Count;i++) {
                ListToReturn.Add(new YelpToReturnDto
                {
                    Name = param.businesses[i].name,
                    Review_Count = param.businesses[i].review_count,
                    Rating = param.businesses[i].rating,
                    Price = param.businesses[i].price
                });
            }
            return ListToReturn;
        }
    }
}