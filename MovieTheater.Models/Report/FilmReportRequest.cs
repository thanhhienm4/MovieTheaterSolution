using AspNetCore.Reporting;
using System;

namespace MovieTheater.Models.Report
{
    public class FilmReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RenderType RenderType { get; set; }
    }
}