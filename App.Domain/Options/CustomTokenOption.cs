using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Options;


public record CustomTokenOption(List<String> Audience , string Issuer  , int AccessTokenExpiration , int RefreshTokenExpiration , string SecurityKey);
