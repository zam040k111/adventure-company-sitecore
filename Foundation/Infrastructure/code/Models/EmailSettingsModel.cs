using System;

namespace Adventure.Foundation.Infrastructure.Models
{
    public class EmailSettingsModel : ICloneable
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Host { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
