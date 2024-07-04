using System;

namespace SAE_SUBNETTING
{
    class Program
    {
        // Added by: Manuel
        // this main method will use all the functions of all classes
        static void Main(string[] args)
        {
            IP ip_object = new IP("237.84.2.178", "255.255.255.0");
            // uses readIPAddress, addressToString
            ip_object.readIPAddress();
            // uses readSubnetMask, addressToString
            ip_object.readSubnetMask();
            // uses showNetworkID, addressToString
            ip_object.showNetworkID();
            // uses showBroadcast, addressToString
            ip_object.showBroadcast();
            // uses ToString, addressToString, Bits.count
            Console.WriteLine($"CIDR notation: {ip_object}");
            // uses writeIPAddress, isIPAddressValid
            ip_object.writeIPAddress();
            // uses readIPAddress, addressToString
            ip_object.readIPAddress();
            // uses writeSubnetMask, isIPAddressValid, isIPAValidSubnetMask
            ip_object.writeSubnetMask();
            // uses readSubnetMask, addressToString
            ip_object.readSubnetMask();
            // uses showNetworkID, addressToString
            ip_object.showNetworkID();
            // uses showBroadcast, addressToString
            ip_object.showBroadcast();
            // uses ToString, addressToString, Bits.count
            Console.WriteLine($"CIDR notation: {ip_object}");
        }
    }
}
