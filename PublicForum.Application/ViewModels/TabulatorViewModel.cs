using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.ViewModels
{
    public class TabulatorViewModel
    {
        public IEnumerable<dynamic> Data { get; set; }
        public int Last_page { get; set; }
        public int Row_count { get; set; }
    }
}
