using BloodTypes.Core.Models;
using BloodTypes.Infrastructure.Repositories;
using Cassandra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace BloodTypes.Infrastructure
{
    public class CassandraDbContext : DbContext
    {
        private readonly string keyspace = "demo";
        private readonly string nodeAddress = "127.0.0.1";

        public PersonRepository People { get; set; }
        public ClusterRepository Clusters { get; set; }
        public BloodAmountRepository BloodAmounts { get; set; }
        public ISession Session { get; private set; }

        //public CassandraDbContext(DbContextOptions<CassandraDbContext> options)
        //    : base(options)
        //{
        //}

        public CassandraDbContext()
        {
            Cluster cluster = Cluster.Builder().AddContactPoint(nodeAddress).Build();
            Session = cluster.Connect(this.keyspace);
            
            People = new PersonRepository(Session);
            BloodAmounts = new BloodAmountRepository(Session);
            Clusters = new ClusterRepository(cluster);
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
