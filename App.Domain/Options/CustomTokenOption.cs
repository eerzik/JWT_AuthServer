using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Options;

public class CustomTokenOption
{
    public List<String> Audience { get; set; }
    public string Issuer { get; set; }

    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }

    public string SecurityKey { get; set; }
}
//public record CustomTokenOption(List<String> Audience , string Issuer  , int AccessTokenExpiration , int RefreshTokenExpiration , string SecurityKey);
