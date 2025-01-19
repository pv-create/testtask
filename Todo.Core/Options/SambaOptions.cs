using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Core.Options
{
    public class SambaOptions
    {
        public const string SectionName = "Samba";

        public string Server { get; set; } = string.Empty;
        public string ShareName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
    }
}