using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public class LineChartVM
    {
        public LineChartVM()
        {
            labels = new List<string>();
            datasets = new List<LineChartChildVM>();
        }
        public List<string> labels { get; set; }
        public List<LineChartChildVM> datasets { get;set; }
    }
    public class LineChartChildVM
    {
        public LineChartChildVM()
        {
            data = new List<int>();
        }
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int borderWidth { get; set; }
        public bool fill { get; set; }
        public List<int> data { get; set; }
    }
}