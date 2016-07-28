using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ReloChatBot.Distance;


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
                strRet = string.Format("It will take about {0} from {1} to {2}", distanceInfo.rows[0].elements[0].distance.text, 
                     distanceInfo.origin_addresses[0], distanceInfo.destination_addresses[0]);
            }
            return strRet;
        }
    }
}