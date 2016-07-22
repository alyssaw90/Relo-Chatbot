using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ReloChatBot;

namespace ReloChatBot.Models
{
    public class CommuteBot : LuisParser
    {
        public CommuteBot(Activity activity, string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=2fb62e84-da20-4205-8dad-d6206e533681&subscription-key=a0794768387a459da34bab6f49878c1e&q=") : base(activity, api_endpoint) { }

    }

    public class CommuteMessageController
    {
        public static async Task<string> IntentsController(LuisInfo luisInfo)
        {
            string responseMessage;
            if (luisInfo.intents.Count() > 0)
            {
                switch (luisInfo.intents[0].intent)
                {
                    case "GetCommuteTime":
                        if (luisInfo.entities.Count() > 0)
                        {
                            responseMessage = await CommuteUtilities.GetCommuteTime(luisInfo.entities[0].entity, luisInfo.entities[1].entity);
                        }
                        else
                        {
                            responseMessage = "Sorry, I don't understand the location.";
                        }
                        break;
                    case "GetDistance":
                        if (luisInfo.entities.Count() > 0)
                        {
                            responseMessage = await CommuteUtilities.GetDistance(luisInfo.entities[0].entity, luisInfo.entities[1].entity);
                        }
                        else
                        {
                            responseMessage = "Sorry, I don't understand the location.";
                        }
                        break;
                    case "GetTransportation":
                        if (luisInfo.entities.Count() > 0)
                        {
                  
                            responseMessage = "Check out this website: http://metro.kingcounty.gov/";
                        }
                        else
                        {
                            responseMessage = "Sorry, I don't understand the location.";
                        }
                        break;
                    default:
                        responseMessage = "Sorry, I don't know how to " + luisInfo.intents[0].intent;
                        break;
                }
            }
            else
            {
                responseMessage = "Sorry, I'm not sure what you want.";
            }

            return responseMessage;
        }

    }
}