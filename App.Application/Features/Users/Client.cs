using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Persistence.Authentication.Configurations;

public record Client(string Id, string Secret, List<String> Audiences);

