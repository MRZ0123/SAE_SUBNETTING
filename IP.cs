using System;
namespace SAE_SUBNETTING
{

    class IP
    {   
        private UInt32? IPAddress;
        private UInt32? subnetMask;


        /// Added: by Manuel
        /// <summary>
        /// Default constructor. Initializes a new instance of the <see cref="IP"/> class.
        /// </summary>
        public IP()
        {
            this.IPAddress = null;
            this.subnetMask = null;
        }

        /// Added by: Manuel
        /// <summary>
        /// Initializes a new instance of the <see cref="IP"/> class.
        /// </summary>
        /// <param name="inputIPAddress">The input IP address.</param>
        /// <param name="inputSubnetMask">The input subnet mask.</param>
        /// <exception cref="ArgumentNullException">Thrown if the input IP address or subnet mask is null.</exception>
        /// <exception cref="FormatException">Thrown if the input IP address or subnet mask is not valid.</exception>
        public IP(string inputIPAddress, string inputSubnetMask)
        {
            // zero the values for the converted IP address and subnet mask
            UInt32 tempIPAddress = 0, tempSubnetMask = 0;
            // check for any null values and throw an exception if so
            _ = inputIPAddress ?? throw new ArgumentNullException(nameof(inputIPAddress));
            _ = inputSubnetMask ?? throw new ArgumentNullException(nameof(inputSubnetMask));
            // check if the input IP address is valid
            if (!isIPAddressValid(inputIPAddress, out tempIPAddress))
            {
                // if not throw an exception
                throw new FormatException("An invalid IP address was given.");
            }
            // check if the input subnet mask is valid
            else if (!(isIPAddressValid(inputSubnetMask, out tempSubnetMask) && isIPAValidSubnetMask(tempSubnetMask)))
            {
                // if not throw an exception
                throw new FormatException("An invalid subnet mask was given.");
            }
            else
            {
                // if both are valid, set the values
                this.IPAddress = tempIPAddress;
                this.subnetMask = tempSubnetMask;
            }
        }

        /// Added by: Jugi
        /// <summary>
        /// Function to read the IP address from class if it exists and display it on the commandline.
        /// </summary>
        public void readIPAddress()
        {
            // only try to read the IP address if it exists
            // otherwise we would get a null reference exception
            // if the IP address is not set
            // display a message on the commandline
            if (this.IPAddress == null)
            {
                Console.WriteLine("No valid IP address given.");
            }
            else
            {
                // display the IP address as a string on the commandline
                Console.WriteLine($"IP address: {addressToString(this.IPAddress)}");
            }
        }

        /// Added by: Sven
        /// <summary>
        /// Function to read the subnet mask from class if it exists and display it on the commandline.
        /// </summary>
        public void readSubnetMask()
        {
            // only try to read the subnet mask if it exists
            // otherwise we would get a null reference exception
            // if the subnet mask is not set
            // display a message on the commandline
            if (this.subnetMask == null)
            {
                Console.WriteLine("No valid subnet mask given.");
            }
            else
            {
                // display the subnet mask as a string on the commandline
                // since subnet masks are pricipially just the same as an IP address
                // the same function can be used to convert the subnet mask to a string
                Console.WriteLine($"Subnet mask: {addressToString(this.subnetMask)}");
            }
        }

        /// Added by: Jugi
        /// <summary>
        /// Function to convert the user's input and write it as IP address into the class.
        /// </summary>
        public void writeIPAddress()
        {
            Console.Write("Enter IP address: ");                // ask the user to input an IP address
            string userInputForIPAddress = Console.ReadLine();  // store the user's input as a string

            UInt32 IPAddressUInt32 = 0;                         // assigning variable here to make sure it is not null and not a garbage value

            // as long as the user's input is not valid, ask them to try again
            while (!isIPAddressValid(userInputForIPAddress, out IPAddressUInt32))
            {
                Console.Write($"No valid IP address given.\nPlease try again: ");   // display message and ask the user to try again
                userInputForIPAddress = Console.ReadLine();                         // store the user's input as a string
            }
            // once the user inputs a valid IP address, store it in the class
            this.IPAddress = IPAddressUInt32;
        }

        /// Added by: Sven
        /// <summary>
        /// Function to convert the user's input and write it as subnet mask into the class.
        /// </summary>
        public void writeSubnetMask()
        {
            Console.Write("Enter subnet mask: ");                   // ask the user to input a subnet mask
            string userInputForSubnetMask = Console.ReadLine();     // store the user's input as a string
            UInt32 subnetMaskUInt32 = 0;                            // assigning variable here to make sure it is not null and not a garbage value

            // since subnet masks are a very specific subset of IP addresses
            // we can use the isIPAddressValid first to check if the user input would be a valid IP address
            // the we can use the converted uint to check extremely efficiently if the user input is a valid subnet mask
            // as long as the user's input is not valid, ask them to try again
            while (!(isIPAddressValid(userInputForSubnetMask, out subnetMaskUInt32) && isIPAValidSubnetMask(subnetMaskUInt32)))   // use && instead of & to make sure we don't process a partially converted uint further and use up precious CPU cycles
            {
                Console.Write($"No valid subnet mask given.\nPlease try again: ");  // display message and ask the user to try again
                userInputForSubnetMask = Console.ReadLine();                        // store the user's input as a string
            }
            // once the user inputs a valid subnet mask, store it in the class
            this.subnetMask = subnetMaskUInt32;
        }

        /// Added by: Jugi
        /// <summary>
        /// Calculates and displays the network ID of the given subnet mask and IP address if they exist.
        /// </summary>
        public void showNetworkID()
        {
            // if either the subnet mask or the IP address is not set, display a message on the commandline
            if (this.subnetMask == null || this.IPAddress == null)
            {
                Console.WriteLine("No valid subnet mask or IP address given.");
            }
            else
            {
                // calculate the network ID using a logical AND operator
                UInt32? networkID = this.IPAddress & this.subnetMask;
                // display the network ID on the commandline
                // using addressToString since the network ID is formatted like any other addres
                Console.WriteLine($"Subnet ID: {addressToString(networkID)}");
            }
        }

        /// Added by: Manuel
        /// <summary>
        /// Calculates and displays the broadcast address of the given subnet mask and IP address if they exist.
        /// </summary>
        public void showBroadcast()
        {
            // if either the subnet mask or the IP address is not set, display a message on the commandline
            if (this.subnetMask == null || this.IPAddress == null)
            {
                Console.WriteLine("No valid subnet mask or IP address given.");
            }
            else
            {
                // if the subnet mask is 0xfffffffe (/31 in CIDR or 255.255.255.254 in decimal), this is a Point-to-Point link
                // see https://datatracker.ietf.org/doc/html/rfc3021 and https://tools.ietf.org/rfc/rfc3021.txt
                if (this.subnetMask == 0xfffffffe)
                {
                    Console.WriteLine($"This is a Point-to-Point link with no broadcast address.");
                }
                else
                {
                    // calculating the broadcast IP
                    // 1. the subnet mask gets inverted (0b_1111_1111_0000_0000 => 0b_0000_0000_1111_1111)
                    // 2. the inverted subnet mask gets compared to the IP address with a logical OR operator
                    // 0b_1011_0010_0000_0011
                    //          |
                    // 0b_0000_0000_1111_1111
                    // this will return the highest possible address in the subnet which is the broadcast address
                    UInt32? broadcast = this.IPAddress | ~this.subnetMask;
                    // display the broadcast address on the commandline
                    Console.WriteLine($"Broadcast address: {addressToString(broadcast)}");
                }
            }
        }

        /// Added by: Manuel
        /// <summary>
        /// Checks if a given IP address is valid.
        /// </summary>
        /// <param name="IPAddressString">The IP address to be tested</param>
        /// <param name="IPAddressUInt32">The IP address converted to a 32-bit unsigned integer</param>
        /// <returns>True if the IP address is valid, false otherwise.</returns>
        private bool isIPAddressValid(string IPAddressString, out UInt32 IPAddressUInt32)
        {
            // set IPAddressUInt32 to 0 to eliminate any potential previous passed values
            IPAddressUInt32 = 0;
            // split the IP address string into an array of octets
            string[] IPAddressStringArray = IPAddressString.Split('.');
            if (IPAddressStringArray.Length != 4) // check if the IP address contains exactly 4 octets
            {
                return false;
            }

            // loop through the 4 octets
            for (int i = 0; i < 4; i++)
            {
                // check if any of the octets contain a leading zero (eg. 192.168.00.1 or 192.168.0.01)
                if (IPAddressStringArray[i].Length > 1 &&
                    IPAddressStringArray[i][0] == '0')
                {
                    return false;
                }

                // check if any of the octets is not a positive integer smaller or eaqual to 255
                if (!UInt32.TryParse(IPAddressStringArray[i], out UInt32 number) || number > 255)
                {
                    return false;
                }
                // because the previous check converts each octet string into an unsigned integer as a byproduct 
                // we can slowly assemble the entire IP address as a 32-bit unsigned integer
                // 1. calculate for how many bits we need to shift the current octet
                // 2. the current octet gets shifted to the left
                // 3. the IP address uint gets set to the result from a logical OR operation of the shifted octet and itself
                IPAddressUInt32 |= (UInt32)(number << (24 - i * 8));
            }
            return true;
        }

        /// Added by: Manuel
        /// <summary>
        /// Checks if a given valid IP is a valid subnet mask.
        /// </summary>
        /// <param name="subnetMask">The subnet mask to be checked.</param>
        /// <returns>True if the subnet mask is valid, false otherwise.</returns>
        private bool isIPAValidSubnetMask(UInt32 subnetMask)
        {
            // as discussed verbally with mr Fricke a host-only route (/32 in CIDR) is not considered a valid subnet mask for our project work
            if (subnetMask == 0xffffffff)
            {
                return false;
            }
            // checking for continuous ones followed by continuous zeros (or just continuous zeros)
            // 1. subnet mask gets inverted (this converts it into the wildcard mask: 0b_1111_1111_0000_0000 => 0b_0000_0000_1111_1111)
            // 2. the wildcard mask gets shifted to the right (0b_0000_0000_1111_1111 => 0b_0000_0000_0111_1111)
            // 3. the shifted wildcard mask gets ANDed with the original subnet mask:
            // 0b_1111_1111_0000_0000
            //             &
            // 0b_0000_0000_0111_1111
            // if the tested subnet mask contains anything else but continuous ones followed by continuous zeros (or just continuous zeros) there will be an overlap with ones in the two masks and therefore the result of the AND comparison will be not zero
            return (subnetMask & (~subnetMask >> 1)) == 0;
        }

        /// Added by: Sven
        /// <summary>
        /// Convert the given UInt32 address to a string representation in the format "x.x.x.x"
        /// </summary>
        /// <param name="address">The address to be converted.</param>
        /// <returns>A string representation of the address.</returns>
        /// <exception cref="NullReferenceException">Thrown if the given address is null.</exception>
        private string addressToString(UInt32? address)
        {
            // circumvent returning "..." if the address is null
            // rather throw a NullReferenceException
            if (address == null)
            {
                throw new NullReferenceException("The given address is null.");
            }

            // return the string representation of the address
            // 1. the address gets shifted to the right by appropriate amount of bits to have the octect in the correct position
            // 2. the resulting octect is ANDed with 0xff to effectively convert it into one byte
            // 3. the resulting byte gets converted into a string
            // 4. the resulting string gets joined with "." to form a string representation of the address
            return string.Join(".", new[] {
                ((address >> 24) & 0xff).ToString(),
                ((address >> 16) & 0xff).ToString(),
                ((address >> 8) & 0xff).ToString(),
                (address & 0xff).ToString()
            });
        }

        /// Added by: Manuel
        /// <summary>
        /// Converts the given IP address and subnet mask into a string representation in the CIDR notation.
        /// </summary>
        /// <returns>A string representation of the CIDR notation of the current IP address and subnet mask.</returns>
        public override string ToString()
        {
            // if none of the fields are set, return an empty string
            // otherwise return the IP address and subnet mask in the format "x.x.x.x/y" with only the set fields filled out
            return this.IPAddress == null && this.subnetMask == null ? "" : $"{(this.IPAddress == null ? "" : addressToString(this.IPAddress))}{(this.subnetMask == null ? "" : $"/{Bits.count((uint)this.subnetMask)}")}";
        }
    }
}