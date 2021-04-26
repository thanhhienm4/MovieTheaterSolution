using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Models.Common.ChartTable
{
    public class ChartData
    {
        public List<string> Lables { get; set; }
        public List<List<decimal>> DataRows {get; set;}
        
        public ChartData()
        {
            Lables = new List<string>();
            DataRows = new List<List<decimal>>();
            DataRows.Add(new List<decimal>());
        }
}
}
