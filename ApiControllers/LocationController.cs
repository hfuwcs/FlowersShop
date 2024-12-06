using FlowersShop.Models.Adress;
using FlowersShop.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlowersShop.ApiControllers
{
    public class LocationController : ApiController
    {
        //LIST
        string urlProvinces = "https://provinces.open-api.vn/api/p/"; //Lấy tỉnh
        string urlDistrics = "https://provinces.open-api.vn/api/d/"; //Lấy quận
        string urlWards = "https://provinces.open-api.vn/api/w/"; //Lấy phường

        [HttpGet]
        [Route("api/Location/GetProvinces")]
        public IHttpActionResult GetProvinces()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlProvinces);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    List<Province> provinces = JsonConvert.DeserializeObject<List<Province>>(data);

                    provinces = provinces.OrderBy(p => p.name).ToList();

                    return Json(provinces);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/Location/GetDistricts")]
        public IHttpActionResult GetDistricts(int province_code)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlDistrics);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    List<District> districts = JsonConvert.DeserializeObject<List<District>>(data);

                    districts = districts.OrderBy(d => d.name).Where(d=>d.province_code == province_code).ToList();

                    return Json(districts);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/Location/GetWards")]
        public IHttpActionResult GetWards(int district_code)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlWards);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    List<Ward> wards = JsonConvert.DeserializeObject<List<Ward>>(data);

                    wards = wards.OrderBy(d => d.name).Where(d => d.district_code == district_code).ToList();

                    return Json(wards);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public string GetProvinceName(string province_code)
        {
            string ProvinceName = "";
            string url = urlProvinces + province_code;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    Province provinces = JsonConvert.DeserializeObject<Province>(data);

                    ProvinceName = provinces.name;
                    return ProvinceName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
        public string GetDistrictName(string district_code)
        {
            string DistrictName = "";
            string url = urlDistrics + district_code;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    District district = JsonConvert.DeserializeObject<District>(data);

                    DistrictName = district.name;

                    return DistrictName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
        public string GetWardName(string ward_code)
        {
            string WardName = "";
            string url = urlWards + ward_code;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                // Gửi request và nhận response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string data = reader.ReadToEnd();

                    Ward ward = JsonConvert.DeserializeObject<Ward>(data);

                    WardName = ward.name;

                    return WardName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }
    }
}
