using System;
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
using ReloChatBot.Controllers;

namespace ReloChatBot
{

    public class LuisParser
    {
        // Spot the python programmer...
        protected LuisClient client;
        protected string raw_result;
        /// <summary>
        /// LUIS API end point
        /// </summary>
        protected string api_endpoint;
        /// <summary>
        /// Activity from ChatBot Framework
        /// </summary>
        protected Activity activity;

        /// <summary>
        /// Questions sent to Relo Chat Bot
        /// and then attached to LUIS API (https://....q=<query>)
        /// </summary>
        public string query;

        /// <summary>
        /// LuisInforData Used to Save Luis String Data
        /// </summary>
        public LuisInfo LuisInfoData;
        /// <summary>
        /// Dictionary of main intents about Relo ChatBot
        /// </summary>
        protected Dictionary<string, string> actions = IntentDirectory.master_actions;

        public JObject json_result;
        /// <summary>
        /// Constructor of LuisParser Class; Turn json result to object
        /// </summary>
        /// <param name="activity">Activity default class of ChatBot Framework</param>
        /// <param name="api_endpoint">LUIS API end point</param>
        public LuisParser(Activity activity, string api_endpoint = "https://api.projectoxford.ai/luis/v1/application?id=3f56e744-90ea-4850-bcd2-759eea1237e7&subscription-key=6171c439d26540d6a380208a16b31958&q=")
        {  
            //question asked by user for ChatBot
            this.query = activity.Text;
            this.activity = activity;
            this.api_endpoint = api_endpoint;
            // instance of LuisClient Class
            this.client = new LuisClient();
            // return LUIS API query json result ad save to raw_result
            this.raw_result = this.client.QueryLuis(this.api_endpoint, query);
            // convert json result into object according to ths blueprint LuisInfo Class; (This is used only by RedirectTransportation intent / CommuteBot )
            this.LuisInfoData = JsonConvert.DeserializeObject<LuisInfo>(this.raw_result);
            // convert LUIS API query json result into JObject (This is used by )
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

        /// <summary>
        /// Overwrite this with your own version
        /// using `public override string Reply {get;}`
        /// </summary>
        public virtual string Reply
        {
            get {
                try
                {
                    return this.actions[this.Intent];
                }
                catch (KeyNotFoundException)
                {
                    return this.Intent + " (No action defined for this Intent).";
                }
            }
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
                // Pass all the routing logic to BotController
                LuisParser masterbot = new LuisParser(activity);
                BotController Router = new BotController(masterbot, activity);
                string result = String.Empty;
                if (Router.masterbot.Intent == "RedirectTransportation")
                {
                    result = await Router.handle_RedirectCommute();
                }
                else
                {
                    result = Router.Reply;
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
