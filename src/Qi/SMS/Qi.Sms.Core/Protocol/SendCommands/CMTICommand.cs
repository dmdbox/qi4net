using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Qi.Sms.Protocol.SendCommands
{
    public class CmtiCommand : AtCommand
    {
        public override string Command
        {
            get { return "CMTI"; }
        }

        public int SmsIndex { get; set; }

        public override bool Init(string content)
        {
            //+CMTI: "SM",1
            bool result = content.Contains("+CMTI:");
            if (result)
            {
                SmsIndex = Convert.ToInt32(Regex.Replace(content, "\\D", ""));

            }
            return result;
        }

        public static int[] GetSmsIndex(string content)
        {
            //+CMTI: "SM",1\r\nsjd+CMTI: "SM",1\r\nCMTI: "SM",1\r\n
            var lines = content.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<int>();

            foreach (var line in lines)
            {
                if (line.Contains("+CMTI"))
                    result.Add(Convert.ToInt32(Regex.Replace(content, "\\D", "")));
            }
            return result.ToArray();
        }
    }
}