using BloodTypes.Core.Models;
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

        public ICollection<HostNode> GetAll()
        {
            var hosts = this.cluster.AllHosts();

            List<HostNode> hostNodes = hosts.Select(host => new HostNode
            {
                Datacenter = host.Datacenter,
                Rack = host.Rack,
                Port = host.Address.Port,
                IpAddress = host.Address.Address.ToString(),
                IsUp = host.IsUp
            }).ToList();

            return hostNodes;
        }
    }
}
