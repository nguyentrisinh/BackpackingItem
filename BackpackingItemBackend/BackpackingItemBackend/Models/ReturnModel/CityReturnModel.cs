using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class CityReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name 
        public string Name { get; set; }
        #endregion

        #region Districts
        public List<DistrictReturnModel> Districts { get; set; }
        #endregion

        public static CityReturnModel Create(City city)
        {
            List<DistrictReturnModel> districts = new List<DistrictReturnModel>();

            if (city.Districts != null)
            {
                foreach (var district in city.Districts)
                {
                    DistrictReturnModel districtReturnItem = DistrictReturnModel.Create(district);
                    districts.Add(districtReturnItem);
                }
            }

            return new CityReturnModel()
            {
                Id = city.Id,
                Name = city.Name,
                Districts = city.Districts == null ? null : districts,
            };
        }
        
        public static List<CityReturnModel> Create(List<City> cities)
        {
            List<CityReturnModel> citiesReturnModel = new List<CityReturnModel>();

            foreach (var city in cities)
            {
                CityReturnModel cityReturnModel = CityReturnModel.Create(city);
                citiesReturnModel.Add(cityReturnModel);
            }

            return citiesReturnModel;
        }
    }
}
