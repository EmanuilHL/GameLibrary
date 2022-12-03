using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Admin
{
    public class HelperAdminServiceModel
    {
        public int HelperId { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
