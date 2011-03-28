namespace Qi.Sms.Encoding
{
    public enum PduType
    {
        Ucs2 = 0x10,
        Bit7 = 0x00,
        Bit8 = 0x01
    }

    public class SmsEncoder
    {
        /// <summary>
        /// Service Center Address
        /// </summary>
        public string SCA { get; set; }
        /// <summary>
        /// Pdu=Type
        /// </summary>
        public PduType PDUType { get; set; }

        /// <summary>
        /// Message Reference
        /// </summary>
        public string MessageReference { get; set; }

        public string OriginatorAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string ProtocolIdentifer
        {
            get;
            set;
        }

        public string DataCodingScheme { get; set; }

        public string ServiceCenterTimeStamp { get; set; }

        public string ValidityPeriod { get; set; }

        public int UserDataLength { get { return UserData.Length; } }

        public string UserData { get; set; }

        public string ToPduContent()
        {
            return SCA+this.PDUType+MessageReference+OriginatorAddress+DestinationAddress
                +ProtocolIdentifer+DataCodingScheme+ServiceCenterTimeStamp
                +ValidityPeriod+UserDataLength+UserData

        }
    }
}