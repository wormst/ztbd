using BloodTypes.Core.Models;
using Cassandra;
using Microsoft.EntityFrameworkCore;

namespace BloodTypes.Infrastructure
{
    public class CassandraDbContext : DbContext
    {
        private readonly string keyspace = "demo";
        private readonly string nodeAddress = "127.0.0.1";

        public PersonRepository People { get; set; }
        public ISession Session { get; private set; }

        //public CassandraDbContext(DbContextOptions<CassandraDbContext> options)
        //    : base(options)
        //{
        //}

        //TODO: consider adding node address in here or keep a list of nodes
        public CassandraDbContext()
        {
            Cluster cluster = Cluster.Builder().AddContactPoint(nodeAddress).Build();
            Session = cluster.Connect(this.keyspace);

            People = new PersonRepository(Session);
        }

        public override void Dispose()
        {
            base.Dispose();
            if(Session != null)
            {
                Session.Dispose();
            }
        }
    }
}
