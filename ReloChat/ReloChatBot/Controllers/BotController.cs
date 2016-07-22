using Microsoft.Bot.Connector;
using ReloChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReloChatBot.Controllers
{
    public class BotController
    {
        const string RedirectLodging = "RedirectLodging";
        const string RedirectCommute = "RedirectTransportation";

        /*Changes Made By CommuteBot*/
        public LuisParser masterbot;

        //private LuisParser masterbot;
        private string userinput;

        private string reply;
        private Activity activity;
        //public string Reply;

        public  BotController(LuisParser masterbot, Activity activity)
        {
            this.masterbot = masterbot;
            this.activity = activity;
            this.userinput = this.activity.Text;
            if (masterbot.RedirectRequired)
            {
                if (masterbot.Intent == RedirectLodging)
                {
                    this.handle_RedirectLodging();
                }
                else
                {
                    this.reply = masterbot.Reply;
                    // this.reply = "No handle created for \"" + this.masterbot.Intent + "\"!";
                    // throw new Exception("No handle created for \"" + this.masterbot.Intent + "\"!");
                }
            } else
            {
                this.reply = masterbot.Reply;
            }
        }
        private void handle_RedirectLodging()
        {
            LodgingBot lobot = new LodgingBot(this.activity);
            this.reply = lobot.Reply;
        }

        /*Changes Made By commuteBot*/
        public async Task<string> handle_RedirectCommute()
        {
            CommuteBot combot = new CommuteBot(this.activity);
            var response = await CommuteMessageController.IntentsController(combot.LuisInfoData);
            return response;
        }

        /*Changes Made By commuteBot*/

        public string Reply
        {
            get { return this.reply; }
        }

    }
}