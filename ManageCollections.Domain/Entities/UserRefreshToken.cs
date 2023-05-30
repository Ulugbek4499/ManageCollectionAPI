using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCollections.Domain.Entities
{
    public class UserRefreshToken: BaseAuditableEntity
    {
        public string? UserEmail { get; set; }
        public string?  RefreshToken { get; set; }
        public bool isActive { get; set; }
        public DateTime ExpiresTime { get; set; }
    }
}
