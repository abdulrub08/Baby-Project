using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baby.Complaince.Entities.BaseModel;

namespace Baby.Complaince.Entities.UserTransaction
{
    public class WinNumberDeclare : Base
    {
        public long GameTypeId { get; set; }
        public string GameTypeName { get; set; }                     
        public int WinningNumber { get; set; }
        
    }
}
