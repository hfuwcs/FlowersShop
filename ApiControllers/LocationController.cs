using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlowersShop.Repository;

namespace FlowersShop.ApiControllers
{
    public class LocationController : Controller
    {
        // Hàm để đọc file JSON từ App_Data
        private List<T> ReadJsonData<T>(string fileName)
        {
            //Cách 1, linh hoạt
            //var filePath = Server.MapPath($"~/App_Data/{fileName}");
            var filePath = "D:\\HK5\\FlowersShop\\App_Data\\" + fileName;
            var jsonData = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        // API để lấy danh sách tỉnh/thành phố
        [HttpGet]
        public ActionResult GetProvinces()
        {
            var provinces = ReadJsonData<Province>("provinces.json");
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }

        // API để lấy danh sách quận/huyện theo mã tỉnh
        [HttpGet]
        public ActionResult GetDistricts(string provinceCode)
        {
            var districts = ReadJsonData<District>("districts.json")
                .Where(d => d.province_code == provinceCode)
                .ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        // API để lấy danh sách phường/xã theo mã quận/huyện
        [HttpGet]
        public ActionResult GetWards(string districtCode)
        {
            var wards = ReadJsonData<Ward>("wards.json")
                .Where(w => w.district_code == districtCode)
                .ToList();
            return Json(wards, JsonRequestBehavior.AllowGet);
        }
        public string GetProvinceName(string code)
        {
            var provinces = ReadJsonData<Province>("provinces.json");
            var province = provinces.FirstOrDefault(p => p.code == code);
            return province.name;
        }
        public string GetDistrictName(string code)
        {
            var districts = ReadJsonData<District>("districts.json");
            var district = districts.FirstOrDefault(d => d.code == code);
            return district.name;
        }
        public string GetWardName(string code)
        {
            var wards = ReadJsonData<Ward>("wards.json");
            var ward = wards.FirstOrDefault(w => w.code == code);
            return ward.name;
        }
    }
}