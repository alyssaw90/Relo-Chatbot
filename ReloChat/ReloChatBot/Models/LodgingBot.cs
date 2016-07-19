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

        public LuisParser masterbot;

        public LodgingBot(Activity activity, string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=8547ee95-9496-43ca-a9a2-b583da92cd7e&subscription-key=6171c439d26540d6a380208a16b31958&q=") : base(activity, api_endpoint) { }
        public override string Reply
        {
            get { return this.GetReply(); }
        }

        private string GetReply()
        {
            LodgingBotFunctionality functionality = new LodgingBotFunctionality(this.Intent, this.activity, this.masterbot);
            return functionality.Reply;
        }

        public void Seed(LuisParser masterbot)
        {
            this.masterbot = masterbot;
        }

    }

    /// <summary>
    /// All the functionality of the bot
    /// </summary>
    public class LodgingBotFunctionality
    {
        private string intent;
        private Activity activity;
        /// <summary>
        /// Temporary variable
        /// </summary>
        private Dictionary<string, string> actions = new Dictionary<string, string>()
        {
            { "DetermineRelocationCost", "The average cost is ..." },
            { "DetermineMoveIsViable", "I think moving for you is a _ idea." },
            { "ElevatorPitch", "Here's the predicament you're in." },
            { "LeapLocationQuestion", "Leap is located in building 86" },
            { "LocationRecomendation", "I think you should move to ..." },
            { "LeapRelocationServices", "At the moment, LEAP will not compensate for any relocation expenses. I can try and help you determine costs though. Would you like to proceed?" },
        };

        private string[] QuestionArray = {
            "Are you interested in Relocating?",
            "Oh cool! Can I help you narrow down your choices?",
        };

        private LuisParser masterbot;

        /*
         * BotStates:
         * string ClientCurrentLocation: Where the client currently claims to live.
         * int ClientRentTarget: How much the client can currently afford.
         * int CommuteTimeRange: Maximum time the user is okay commuting;
         * bool AccessToCar: Does the user have access to a car;
         * string CityPreference: User's current city preference;
         * bool ClientInterestedInRelo: User wants to use relocation services;
         * bool AskedClientAboutRelo: User already Declined;
         * int LastQuestion: A key of the last question that was asked;
         */

        /// <summary>
        /// Where all the magic happens, where state will be kept and whatnot.
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="activity"></param>
        /// <param name="masterbot"></param>
        public LodgingBotFunctionality(string intent, Activity activity, LuisParser masterbot)
        {
            this.intent = intent;
            this.activity = activity;
            this.masterbot = masterbot;
        }

        public string Reply
        {
            get
            {
                return this.GetReply();
            }
        }

        /// <summary>
        /// All these GET/SET stuff is for maintianing the State of the bot
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        private int GetIntProperty(string key)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            return userData.GetProperty<int>(key);
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

        private void SetProperty(string key, int value)
        {
            StateClient stateClient = this.activity.GetStateClient();
            BotData userData = stateClient.BotState.GetUserData(this.activity.ChannelId, this.activity.From.Id);
            userData.SetProperty<int>(key, value);
            stateClient.BotState.SetUserData(this.activity.ChannelId, this.activity.From.Id, userData);
        }

        /// <summary>
        /// Generate a reply
        /// </summary>
        /// <returns>The string to be shipped to the client</returns>
        private string GetReply()
        {
            // Manipulate bot state and generate a reply

            //bool test = !this.GetBoolProperty("AskedClientAboutRelo");

            // If we haven't asked the client and relocation and the last question was not the relocation question
            if (!this.GetBoolProperty("AskedClientAboutRelo") && this.GetIntProperty("LastQuestion") != 0)
            {
                this.SetProperty("LastQuestion", 0);
                return this.QuestionArray[0];
            // If the last question was the relocation question
            } else if (this.GetIntProperty("LastQuestion") == 0)
            {
                // and the user said yes...
                if (this.masterbot.Intent == "PositiveConfirmation")
                {
                    this.SetProperty("LastQuestion", 1);
                    this.SetProperty("AskedClientAboutRelo", true);
                    this.SetProperty("ClientInterestedInRelo", true);
                    return this.QuestionArray[1];
                // and if the user said no...
                } else if (this.masterbot.Intent == "NegativeConfirmation")
                {
                    this.SetProperty("LastQuestion", -1);
                    this.SetProperty("AskedClientAboutRelo", true);
                    this.SetProperty("ClientInterestedInRelo", false);
                    return "Aww. Well sorry about that.";
                // if the user doesn't make sense...
                } else
                {
                    return "Come again?";
                }
            // if the user is interested in relocation help...
            } else if (this.GetBoolProperty("ClientInterestedInRelo")) {
                return "I am going to help you find a new place.";
            // and if all else fails...
            } else
            {
                this.SetProperty("LastQuestion", -1);
                return "default";
            }

        }
    }

}
