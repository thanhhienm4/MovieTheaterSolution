using System.Collections.Generic;

namespace MovieTheater.Models.Common.ChartTable
{
    public class ChartData
    {
        public List<string> Lables { get; set; }
        public List<List<decimal>> DataRows { get; set; }

        public ChartData()
        {
            Lables = new List<string>();
            DataRows = new List<List<decimal>>();
            DataRows.Add(new List<decimal>());
        }
    }
}