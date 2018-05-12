using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypes.Infrastructure.Repositories
{
    public class ClusterRepository
    {
        private readonly Cluster cluster;

        public ClusterRepository(Cluster cluster)
        {
            this.cluster = cluster;
        }

        public ICollection<Host> GetAll()
        {
            return this.cluster.AllHosts();
        }
    }
}
