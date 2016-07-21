using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ReloChatBot.Distance;
using ReloChatBot.Transportation;

namespace ReloChatBot
{
    //Methods that will retrieve the correct commute info.
    public class CommuteUtilities
    {
        //Get Commute Time for Cars
        public static async Task<string> GetCommuteTime(string origin, string destination)
        {
            string strRet = string.Empty;
            DistanceInfo distanceInfo = await GoogleAPIs.GetDistanceInfoAsync(origin, destination);
            if (null == distanceInfo)
            {
                strRet = string.Format("Sorry, I could not get the commute time for {0} to {1}", origin, destination);
            }
            else
            {
                strRet = string.Format("It will take about {0}", distanceInfo.rows[0].elements[0].duration.text);
            }
            return strRet;
        }

        //Get Commute Distance for Cars
        public static async Task<string> GetDistance(string origin, string destination)
        {
            string strRet = string.Empty;
            DistanceInfo distanceInfo = await GoogleAPIs.GetDistanceInfoAsync(origin, destination);
            if (null == distanceInfo)
            {
                strRet = string.Format("Sorry, I could not get the distance for {0} to {1}", origin, destination);
            }
            else
            {
                strRet = string.Format("It will take about {0}", distanceInfo.rows[0].elements[0].distance.text);
            }
            return strRet;
        }

        //Get Transportation
        public static async Task<string> GetTransportation(string origin, string destination)
        {
            string strRet = string.Empty;
            TransportationInfo transportationInfo = await GoogleAPIs.GetTransportationInfoAsync(origin, destination);
            if (null == transportationInfo)
            {
                strRet = string.Format("Sorry, I could not get the transportion info from {0} to {1}", origin, destination);
            }
            else
            {
                strRet = string.Format("Here is transportation information: {0}",
                    transportationInfo.routes[0].legs[0].steps[1].transit_details.line.short_name);
                    //transportationInfo.routes[0].legs[0].steps[1].transit_details.line.agencies[0].short_name);
                    //,transportationInfo.routes[0].legs[0].steps[0].transit_details.line.agencies[0].url);
            }
            return strRet;
        }
    }
}