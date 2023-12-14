using CrudOperation_HttpClient_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrudOperation_HttpClient_.Controllers
{
    public class CrudController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44302/api");
        private readonly HttpClient _client;

        public CrudController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Read()
        {
            ReadRecordViewModel responseObject = null;

            List<ReadRecordViewModel> readList = new List<ReadRecordViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/CrudOperation/ReadRecord").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                //readList = JsonConvert.DeserializeObject<List<ReadRecordViewModel>>(data);
              
                responseObject = JsonConvert.DeserializeObject<ReadRecordViewModel>(data);
                if (responseObject != null && responseObject.readRecordData != null)
                {
                    foreach (var record in responseObject.readRecordData)
                    {
                        var id = record.Id;
                        var userName = record.UserName;
                        var age = record.Age;
                    }
                }

                
            }
            return View(responseObject.readRecordData);

        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateRecordViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/CrudOperation/CreateRecord", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Record is created";
                    return RedirectToAction("Read");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                throw;
            } 

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                GetRecordViewModel model = new GetRecordViewModel();
                HttpResponseMessage response =  _client.GetAsync(_client.BaseAddress + "/CrudOperation/GetRecord/" + id).Result;
               
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        model = JsonConvert.DeserializeObject<GetRecordViewModel>(data);
                        if(model != null && model.GetRecordData != null)
                        {
                        foreach (var record in model.GetRecordData)
                        {
                            var idd = record.Id;
                            var userName = record.UserName;
                            var age = record.Age;
                        }

                        }
                    }
                    return View(model.GetRecordData);
               
              
            }
            catch (Exception ex)
            { 
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost]

        public IActionResult Edit(UpdateViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/CrudOperation/UpdateRecord", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Record is updated";
                    return RedirectToAction("Read");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                throw;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            GetRecordViewModel model = new GetRecordViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/CrudOperation/GetRecord/" + id).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<GetRecordViewModel>(data);
                    if (model != null && model.GetRecordData != null)
                    {
                        foreach (var record in model.GetRecordData)
                        {
                            var idd = record.Id;

                        }

                    }
                }
                return View(model.GetRecordData);
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpPost,ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
         
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/CrudOperation/DeleteRecord/" + id).Result;
            try
            {
                //var request = new HttpRequestMessage
                //{
                //    Method = HttpMethod.Delete,
                //    RequestUri = new Uri(_client.BaseAddress + $"/CrudOperation/DeleteRecord/{id}"),
                //    Content = new StringContent("", Encoding.UTF8, "application/json")
                //};
                //HttpResponseMessage response = _client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Record is deleted";
                    return RedirectToAction("Read");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to delete record";
                    return RedirectToAction("Read");
                }
            }
           
             catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                throw;
            }

            return View();

           
        }
    }
}
