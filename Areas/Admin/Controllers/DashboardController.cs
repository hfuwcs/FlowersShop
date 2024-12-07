using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Models;
using FlowersShop.Repository;
namespace FlowersShop.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Home
        static QL_BanHoaEntities db = new QL_BanHoaEntities();
        public ActionResult Index()
        {
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            ViewBag.DonHang = ordersInCurrentWeek();
            List<double?> income = new List<double?>();
            income.Add(GetIncomeByYear(currentYear));
            List<IncomeModel> incomeByMonth = GetIncomeByMonth(currentYear);

            return View();
        }

        public JsonResult JsonIncomeDataSet()
        {
            List<IncomeModel> incomeByMonth = GetIncomeByMonth(DateTime.Now.Year);

            int currentMonth = DateTime.Now.Month;

            List<int> lastSixMonths = Enumerable.Range(0, 6)
                                        .Select(i => (currentMonth - i - 1 + 12) % 12 + 1)
                                        .ToList();

            var filteredIncome = incomeByMonth.Where(income => lastSixMonths.Contains(income.month));

            return Json(filteredIncome, JsonRequestBehavior.AllowGet);
        }

        public double? GetIncomeByYear(int year)
        {
            List<Order> orders = db.Order.ToList();
            DateTime currentDate = DateTime.Now;

            // Thiết lập lịch theo chuẩn vi-VN
            Calendar calendar = new CultureInfo("vi-VN", false).Calendar;

            int currentYear = currentDate.Year;
            int previousYear = currentYear - 1;
            int twoYearsAgo = currentYear - 2;

            double? incomeByYear = 0;

            incomeByYear = orders
                .Where(order => order.Order_Date != null && order.Order_Date.Value.Year == currentYear && order.Status == "Thành công")
                .Sum(order => order.Total_Amount);
            return incomeByYear;
        }
            public List<IncomeModel> GetIncomeByMonth(int year)
            {
                List<Order> orders = db.Order.ToList();
                int currentYear = year;
                var ordersInCurrentYear = orders.Where(order =>
                {
                    if (order.Order_Date != null)
                    {
                        DateTime orderDate = order.Order_Date.Value;
                        return orderDate.Year == currentYear;
                    }
                    return false;
                }).ToList();

                var incomeByMonth = new List<IncomeModel>();
                for (int i = 1; i <= 12; i++)
                {
                    double? income = ordersInCurrentYear
                        .Where(order => order.Order_Date != null && order.Order_Date.Value.Month == i && order.Status == "Thành công")
                        .Sum(order => order.Total_Amount);
                    incomeByMonth.Add(new IncomeModel { month = i, income = income });
                }

                return incomeByMonth;
            }
        public int ordersInCurrentWeek()
        {
            List<Order> orders = db.Order.ToList();
            DateTime currentDate = DateTime.Now;

            // Thiết lập lịch theo chuẩn vi-VN
            Calendar calendar = new CultureInfo("vi-VN", false).Calendar;

            // Lấy số tuần hiện tại
            int currentWeek = calendar.GetWeekOfYear(currentDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            int currentYear = currentDate.Year;
            var ordersInCurrentWeek = orders.Where(order =>
            {
                if (order.Order_Date != null)
                {
                    DateTime orderDate = order.Order_Date.Value;
                    int orderWeek = calendar.GetWeekOfYear(orderDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                    return orderWeek == currentWeek && orderDate.Year == currentYear;
                }
                return false;
            }).ToList();

            return ordersInCurrentWeek.Count;
        }

        //Sau tôi lại viết cái method này nhỉ
        public ActionResult Modal()
        {
            return View();
        }
        public ActionResult DrawTotalRevenueLineChart()
        {
            LineChartVM model = new LineChartVM();

            model = GetLineChartData();
            return View(model);
        }
        private LineChartVM GetLineChartData()
        {
            List<string> months = Helpers.MonthName();

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