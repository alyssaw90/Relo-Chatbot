﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using ReloChatBot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ReloChatBot.Models;

namespace ReloChatBot
{

    public class LuisParser
    {
        // Spot the python programmer...
        protected LuisClient client;
        protected string raw_result;
        protected string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=3f56e744-90ea-4850-bcd2-759eea1237e7&subscription-key=6171c439d26540d6a380208a16b31958&q=";

        protected Dictionary<string, string> actions = IntentDirectory.master_actions;

        public JObject json_result;

        public LuisParser(string query)
        {
            this.client = new LuisClient();
            this.raw_result = this.client.QueryLuis(this.api_endpoint, query);
            this.JsonResult();
        }

        protected void JsonResult()
        {
            this.json_result = JObject.Parse(this.raw_result);
        }

        public string Intent
        {
            get { return json_result["intents"][0]["intent"].ToString(); }
        }

        public bool RedirectRequired
        {
            get { return this.Intent.StartsWith("Redirect"); }
        }

        public string Reply
        {
            get { return this.Intent; }
        }

    }

    // [BotAuthentication]
    public class MessagesController : ApiController
    {

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                // create one big string
                string result = "";

                LuisParser masterbot = new LuisParser(activity.Text);
                result += masterbot.Reply;

                if (masterbot.RedirectRequired)
                {
                    // Okay figure out what redirection they need
                    if (masterbot.Intent == "RedirectLodging")
                    {
                        // lobot redirect
                        LodgingBot lobot = new LodgingBot(activity.Text);
                        result += ". Loding doing things";
                    } 
                    else
                    {
                        result += ". Unsure of user intent ({masterbot.Intent}).";
                    }
                }

                Activity reply = activity.CreateReply(result);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
                //ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                //Activity reply = message.CreateReply("Are you typing?");
                //connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}