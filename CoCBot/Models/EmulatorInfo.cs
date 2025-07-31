using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCBot.Models
{
    public record EmulatorInfo(string DeviceId, string Status)
    {
        public override string ToString() => $"{DeviceId} ({Status})";
    }
}
