using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using SmsVote.Web.Domain;
using Twilio.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace SmsVote.Web.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly VoteRepository _db;

        public SmsController()
        {
            _db = new VoteRepository();
        }

        [HttpGet]
        public JsonResult Stats()
        {
            return Json(_db.GetSummary(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateRequest(PrivateData.TwilioAuthToken)]
        public ActionResult Vote(SmsRequest request)
        {
            TwilioResponse response = new TwilioResponse();
            string message;
            string vote = request.Body.Trim();

            if (string.IsNullOrEmpty(vote))
            {
                message = "Sorry!  Your vote was ignored because it was missing or contained only whitespace.";
            }
            else
            {
                bool updated = _db.AddOrUpdateVote(DateTimeOffset.UtcNow, request.From, request.Body);
                message = "Thanks for voting.  Your vote was " + (updated ? "updated!" : "recorded!");
            }

            return TwiML(response.Sms(message));
        }

        [HttpPost]
        public void Reset()
        {
            _db.ClearVotes();
        }
    }
}