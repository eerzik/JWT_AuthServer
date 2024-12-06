using App.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities.Identity
{
    public class UserApp : IdentityUser, IAuditEntity
    {
        //Ek kullanmak istediğimiz alanlar eklenecek
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
