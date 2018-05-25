namespace BloodTypes.Core.Models
{
    public class HostNode
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Datacenter { get; set; }
        public string Rack { get; set; }
        public bool IsUp { get; set; }
    }
}
