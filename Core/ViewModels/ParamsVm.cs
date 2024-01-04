using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ParamsVm
    {
        public string? customerName { get; set; }
        public string? employeeName { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
