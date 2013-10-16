using System;

namespace SmsVote.Web.Domain.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public DateTimeOffset When { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
    }
}