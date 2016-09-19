using System;
using System.Linq;

using Google.Maps.Geocoding;

/*
 * See https://github.com/ericnewton76/gmaps-api-net
 * Dependent on
 * - Google.Maps.dll
 * - NewtonSoft.Json (6+)
 */
namespace WebPortal.Utils
{
    public static class GeoUtils
    {
        // Returns (latitude,longitude)
        public static Tuple<string, string> AddressToCoordinates(string address, string zip, string city)
        {
            if (!String.IsNullOrWhiteSpace(address) && !String.IsNullOrWhiteSpace(city))
            {
                GeocodingRequest request = new GeocodingRequest();
                request.Address = address + " " + zip + " " + city;
                request.Sensor = false;
                GeocodeResponse response = new GeocodingService().GetResponse(request);
                Result result = response.Results.FirstOrDefault();
                if (result != null)
                {
                    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    return new Tuple<string, string>(
                        result.Geometry.Location.Latitude.ToString(culture),
                        result.Geometry.Location.Longitude.ToString(culture));
                }
            }
            return new Tuple<string, string>("0", "0");
        }
    }
}