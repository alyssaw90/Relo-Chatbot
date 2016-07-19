using Microsoft.Bot.Connector;
using ReloChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Controllers
{
    public class BotController
    {
        const string RedirectLodging = "RedirectLodging";
        const string RedirectCommute = "RedirectTransportation";

        private LuisParser masterbot;
        private string userinput;

        private string reply;
        private Activity activity;

        public BotController(LuisParser masterbot, Activity activity)
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
                if (masterbot.Intent == RedirectCommute)
                {
                    this.handle_RedirectCommute();
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

        private void handle_RedirectCommute()
        {
            CommuteBot combot = new CommuteBot(this.activity);
            this.reply = combot.Reply;
        }
        public string Reply
        {
            get { return this.reply; }
        }

    }
}