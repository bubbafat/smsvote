using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsVote.Web.Domain.Models;

namespace SmsVote.Web.Domain
{
    public class VoteRepository : IDisposable
    {
        private readonly SmsVoteDbContext _db;
        private readonly object _dbLock = new object();

        public VoteRepository()
        {
            _db = new SmsVoteDbContext();
        }

        public bool AddOrUpdateVote(DateTimeOffset when, string from, string vote)
        {
            bool updated = false;
            lock (_dbLock)
            {
                var existingVote = _db.Votes.FirstOrDefault(v => v.From == from);
                if (existingVote == null)
                {
                    existingVote = new Vote
                        {
                            Content = vote,
                            From = from,
                            When = when,
                        };

                    _db.Votes.Add(existingVote);
                }
                else
                {
                    updated = true;
                    existingVote.Content = vote;
                    existingVote.When = when;
                }

                _db.SaveChanges();
            }

            return updated;
        }

        public Dictionary<string, int> GetSummary()
        {
            lock (_dbLock)
            {
                var query = from v in _db.Votes
                            group v by v.Content into g
                            select new {Value = g.Key, Votes = g.Count()};

                return query.ToDictionary(v => v.Value, v => v.Votes);
            }
        }

        public void ClearVotes()
        {
            lock (_dbLock)
            {
                foreach (Vote v in _db.Votes.ToList())
                {
                    _db.Votes.Remove(v);
                }

                _db.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (_db) { }
        }
    }
}
