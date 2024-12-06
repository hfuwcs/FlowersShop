using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
namespace FlowersShop.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Modal()
        {
            return View();
        }
        public ActionResult DrawLineChart()
        {
            LineChartVM model = new LineChartVM();

            model = GetLineChartData();
            return View(model);
        }
        private LineChartVM GetLineChartData()
        {
            List<string> months = Helpers.Months();

            LineChartVM lineChartData = new LineChartVM();

            var labels = new List<string>();
            labels.Add("Red");
            labels.Add("Blue");
            labels.Add("Yellow");
            
            var datasets = new List<LineChartChildVM>();
            LineChartChildVM childModel = new LineChartChildVM();

            childModel.label = "Color data";
            childModel.borderWidth = 1;
            childModel.backgroundColor = "rgba(255, 99, 132, 0.5)";
            childModel.borderColor = "rgba(255, 99, 132, 1)";
            childModel.fill = true;

            var datalist = new List<int>();

            foreach(var label in labels)
            {
                if (label == "Red")
                {
                    datalist.Add(1);
                }
                else if (label == "Blue")
                {
                    datalist.Add(2);
                }
                else if (label == "Yellow")
                {
                    datalist.Add(3);
                }
            }
            childModel.data = datalist;
            datasets.Add(childModel);

            lineChartData.datasets = datasets;
            lineChartData.labels = labels;

            return lineChartData;
        }

    }
}