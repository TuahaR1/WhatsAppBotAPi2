using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WhatsAppBotAPi.Services.Response;

namespace WhatsAppBotAPi.Services.Helpers
{
    public static class CommonHelper
    {
        /// <summary>
		/// This is to prep the phone number to be in the correct length and form for whatsapp.
        /// 1. If the Phone number is Null return -1
        /// 2. Number must start with the country code assume South Africa (27)
        /// 3. For South Africa the number must be minimum 9 digits without the country code and any leading zeros if less return -1
        /// 4. If the number is more than 9 digits then assume the number is correct
		/// </summary>
		/// <param name="phoneNumber"></param>
		/// <returns>String: a formated number for WhatsApp</returns>
		
        public static string PrepNumber(string phoneNumber)
        {
            // Step 1: Strip out any character that is not a digit
            string digitsOnly = phoneNumber?.Where(char.IsDigit).Any() == true ? new string(phoneNumber.Where(char.IsDigit).ToArray()) : "-1";

            // Step 2: If the first character is a 0, then Remove it
            if (digitsOnly.Length > 9 && digitsOnly[0] == '0')
            { // Remove any leading zero
                digitsOnly = digitsOnly.Remove(0, 1);
            }

            // Test the length of the number with a switch
            return digitsOnly.Length switch
            {
                < 9 => "-1",// Return -1 if the length is less than 9
                9 => "27" + digitsOnly,// Add 27 in front of the number if the length is 9
                _ => digitsOnly,// Return the number if the length is more than 9
            };
        }

        public static string GetWAMId(WhatsAppResponse whatsAppResponse)
        {
            // This is to try and get the WAMID

            StringBuilder msgIDsBuilder = new StringBuilder();

            foreach (Message msg in whatsAppResponse.Messages)
            {
                msgIDsBuilder.Append(msg.Id);
                msgIDsBuilder.Append(", ");
            }

            return msgIDsBuilder.ToString();
        }

    }
}
