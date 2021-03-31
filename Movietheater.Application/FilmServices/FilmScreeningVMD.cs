using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movietheater.Application.FilmServices
{
    public class FilmScreeningVMD
    {
        public string Name { get; set; }
        public int Id { get; set; }

        List<ScreeningVMDLite> Screenings { get; set; }

    }
    partial class ScreeningVMDLite
    {
         public int Id { get; set;}
         public DateTime TimeStart { get; set; }
    }
}
