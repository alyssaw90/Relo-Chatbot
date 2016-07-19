using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Models
{
    public class LodgingBot : LuisParser
    {
        // Spot the python programmer again...
    
        // The only way to get this to work, and not inherit the RESULTS from LuisParser (parent class)
        // Is to plug in api_endpoint as one of the paramters, and set a default

        public LodgingBot(Activity activity, string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=8547ee95-9496-43ca-a9a2-b583da92cd7e&subscription-key=6171c439d26540d6a380208a16b31958&q=") : base(activity, api_endpoint) { }
        public override string Reply
        {
            get { return this.GetReply(); }
        }

        private string GetReply()
        {
            LodgingBotFunctionality functionality = new LodgingBotFunctionality(this.Intent, this.activity);
            return functionality.Reply;
        }


    }

    /// <summary>
    /// All the functionality of the bot
    /// </summary>
    public class LodgingBotFunctionality
    {
        private string intent;
        private Activity activity;

        private Dictionary<string, string> actions = new Dictionary<string, string>()
        {
            { "DetermineRelocationCost", "The average cost is ..." },
            { "DetermineMoveIsViable", "I think moving for you is a _ idea." },
            { "ElevatorPitch", "Here's the predicament you're in." },
            { "LeapLocationQuestion", "Leap is located in building 86" },
            { "LocationRecomendation", "I think you should move to ..." }
        };

        /// <summary>
        /// Where all the magic happens, where state will be kept and whatnot.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="activity"></param>
        public LodgingBotFunctionality(string intent, Activity activity)
        {
            this.intent = intent;
            this.activity = activity;
        }

        public string Reply
        {
            //get { return this.actions[this.intent]; }
            get { return this.GetReply(); }
        }

        private string GetReply()
        {
            StateClient stateclient = this.activity.GetStateClient();
            BotData data = stateclient.BotState.GetConversationData(activity.ChannelId, activity.From.Id);

            
            if (data.GetProperty<string>("test") == "test")
            {
                return "Something ain't right...";
            } else
            {
                data.SetProperty("test", "test");
                return "Well.. something changed!";
            }
        }
    }

}
