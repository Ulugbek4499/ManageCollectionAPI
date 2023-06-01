using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCollections.Application.Models.Token
{
    public class UserRefreshToken
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        //public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
