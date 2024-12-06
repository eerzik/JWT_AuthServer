using App.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Identity;

public class UserRefreshToken : BaseEntity<int>,IAuditEntity
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
    public DateTime Expiration { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}
