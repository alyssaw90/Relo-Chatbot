using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReloChatBot.Models
{
    public class QuestionModel
    {
        public string question;
        private Dictionary<string, QuestionModel> branches;
        private LuisParser provider;

        public QuestionModel(string question, LodgingBot provider)
        {
            this.question = question;
            this.provider = provider;
        }

        public QuestionModel(string question, LuisParser provider)
        {
            this.question = question;
            this.provider = provider;
        }

        public void AddBranch(string intent, QuestionModel value)
        {
            this.branches[intent] = value;
        }

        public QuestionModel GetBranch(string intent)
        {
            return this.branches[intent];
        }
    }
}