using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Reporting;

namespace MovieTheater.Models.Report
{
    public class FilmReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RenderType RenderType { get; set; }
    }
}
