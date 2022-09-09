using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ATMSoftware.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public string FullName { get; set; }
        public string BranchName { get; set; }
        public float TotalBalance { get; set; }
        public int CardPin { get; set; }
    }
}
