using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmsVote.Web.Domain.Models;

namespace SmsVote.Web.Domain
{
    public sealed class SmsVoteDbContext : DbContext
    {
        public SmsVoteDbContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Vote> Votes { get; set; }
    }
}
