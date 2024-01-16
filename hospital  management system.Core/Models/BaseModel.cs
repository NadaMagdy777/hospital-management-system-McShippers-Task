using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Models
{
    public class BaseModel
    {
        public bool IsDeleted { get; set; } = false;
        public int ID { get; set; }
    }
}
