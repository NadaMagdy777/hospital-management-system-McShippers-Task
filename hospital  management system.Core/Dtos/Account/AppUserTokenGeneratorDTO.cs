using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Account;
public class AppUserTokenGeneratorDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ID { get; set; }
    public ApplicationUser User { get; set; }
}
