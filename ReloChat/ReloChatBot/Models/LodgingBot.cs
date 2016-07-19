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

        /*
         * BotStates: 
         * 
         * 
         */

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
            get
            {
                return this.GetReply();
            }
        }

        private string GetProperty(string key)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            return userData.GetProperty<string>(key);
        }

        private bool GetBoolProperty(string key)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            return userData.GetProperty<bool>(key);
        }

        private void SetProperty(string key, string value)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            userData.SetProperty<string>(key, value);
            stateClient.BotState.SetUserData(this.activity.ChannelId, this.activity.From.Id, userData);
        }

        private void SetProperty(string key, bool value)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            userData.SetProperty<bool>(key, value);
            stateClient.BotState.SetUserData(this.activity.ChannelId, this.activity.From.Id, userData);
        }

        private string GetReply()
        {
            // Manipulate bot state and generate a reply

            

            if (this.GetBoolProperty("test"))
            {
                return "TEST WAS SET";
            } else
            {
                this.SetProperty("test", true);
                return "TEST WAS NOT SET";
            }
        }
    }

}
