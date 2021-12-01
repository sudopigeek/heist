using System;

namespace heist
{
    public class Bank
    {
        public int CashOnHand { get; set; }
        public int AlarmScore { get; set; }
        public int VaultScore { get; set; }
        public int SecurityGuardScore { get; set; }
        public bool IsSecure
        {
            get { return (AlarmScore + VaultScore + SecurityGuardScore) is <= 0 ? false : true; }
        }

    }
}