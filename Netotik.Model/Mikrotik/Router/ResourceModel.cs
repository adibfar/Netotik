namespace Netotik.ViewModels.Mikrotik
{
    public class Router_ResourceModel
    {
        public string Uptime { get; set; }

        public string Version { get; set; }

        public string Build_time { get; set; }

        public string Free_memory { get; set; }
        public string Total_memory { get; set; }
        public string Cpu { get; set; }
        public string Cpu_count { get; set; }
        public string Cpu_frequency { get; set; }
        public string Cpu_load { get; set; }
        public string Free_hdd_space { get; set; }
        public string Total_hdd_space { get; set; }
        public string Architecture_name { get; set; }
        public string Board_name { get; set; }
        public string Platform { get; set; }
    }
}
