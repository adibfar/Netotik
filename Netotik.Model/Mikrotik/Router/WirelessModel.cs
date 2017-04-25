namespace Netotik.ViewModels.Mikrotik
{
    public class Router_WirelessModel
    {
        public string id { get; set; }

        public string default_name { get; set; }

        public string name { get; set; }

        public string mtu { get; set; }
        public string l2mtu { get; set; }
        public string mac_address { get; set; }
        public string arp { get; set; }
        public string arp_timeout { get; set; }
        public string disable_running_check { get; set; }
        public string interface_type { get; set; }
        public string radio_name { get; set; }
        public string mode { get; set; }
        public string ssid { get; set; }
        public string area { get; set; }
        public string frequency_mode { get; set; }
        public string country { get; set; }
        public string antenna_gain { get; set; }
        public string frequency { get; set; }
        public string band { get; set; }
        public string channel_width { get; set; }
        public string scan_list { get; set; }
        public string wireless_protocol { get; set; }
        public string rate_set { get; set; }
        public string supported_rates_b { get; set; }
        public string supported_rates_a_g { get; set; }

        public string basic_rates_b { get; set; }
        public string basic_rates_a_g { get; set; }
        public string max_station_count { get; set; }
        public string distance { get; set; }
        public string tx_power_mode { get; set; }
        public string noise_floor_threshold { get; set; }
        public string nv2_noise_floor_offset { get; set; }
        public string vlan_mode { get; set; }
        public string vlan_id { get; set; }
        public string wds_mode { get; set; }
        public string wds_default_bridge { get; set; }
        public string wds_default_cost { get; set; }
        public string wds_cost_range { get; set; }
        public string wds_ignore_ssid { get; set; }
        public string update_stats_interval { get; set; }
        public string bridge_mode { get; set; }
        public string default_authentication { get; set; }

        public string default_forwarding { get; set; }
        public string default_ap_tx_limit { get; set; }
        public string default_client_tx_limit { get; set; }
        public string wmm_support { get; set; }
        public string hide_ssid { get; set; }
        public string security_profile { get; set; }
        public string interworking_profile { get; set; }
        public string wps_mode { get; set; }
        public string station_roaming { get; set; }
        public string disconnect_timeout { get; set; }
        public string on_fail_retry_time { get; set; }
        public string preamble_mode { get; set; }
        public string compression { get; set; }
        public string allow_sharedkey { get; set; }
        public string station_bridge_clone_mac { get; set; }
        public string ampdu_priorities { get; set; }
        public string guard_interval { get; set; }
        public string ht_supported_mcs { get; set; }
        public string tx_chains { get; set; }
        public string rx_chains { get; set; }
        public string amsdu_limit { get; set; }
        public string amsdu_threshold { get; set; }
        public string tdma_period_size { get; set; }
        public string nv2_queue_count { get; set; }
        public string nv2_qos { get; set; }
        public string nv2_cell_radius { get; set; }
        public string nv2_security { get; set; }
        public string nv2_preshared_key { get; set; }
        public string hw_retries { get; set; }
        public string frame_lifetime { get; set; }
        public string adaptive_noise_immunity { get; set; }
        public string hw_fragmentation_threshold { get; set; }
        public string hw_protection_mode { get; set; }
        public string hw_protection_threshold { get; set; }
        public string frequency_offset { get; set; }
        public string rate_selection { get; set; }
        public string multicast_helper { get; set; }
        public string multicast_buffering { get; set; }
        public string keepalive_frames { get; set; }
        public string running { get; set; }
        public string disabled { get; set; }

    }
}
