using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DSCC.CW1._13626.FRONT.Models;
using System.Net;

namespace DSCC.CW1._13626.FRONT.Controllers
{
    public class CustomerController : Controller
    {
            private readonly string baseUrl = "https://localhost:7006/api/Customer";

            // GET: Food
            public async Task<ActionResult> Index()
            {
                List<Customer> customerList = new List<Customer>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        customerList = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to retrieve Customers";
                    }
                }

                return View(customerList);
            }


        // GET: Customer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "ID is required.");
            }

            Customer customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve Customer details";
                }
            }

            if (customer == null)
            {
                return HttpNotFound("Customer not found.");
            }

            return View(customer);
        }



        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Customer customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to retrieve Customer details";
                    return RedirectToAction("Index");
                }
            }

            return View(customer);
        }


        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
