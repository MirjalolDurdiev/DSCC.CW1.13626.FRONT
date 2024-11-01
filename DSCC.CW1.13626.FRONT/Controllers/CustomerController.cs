﻿using Newtonsoft.Json;
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
using System.Text;

namespace DSCC.CW1._13626.FRONT.Controllers
{
    public class CustomerController : Controller
    {
            private readonly string baseUrl = "https://localhost:7006/api/Customer";

            // GET: Customer
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
        public async Task<ActionResult> Details(int id)
        {
            Customer customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                }
                else
                {
                    return HttpNotFound("Customer not found.");
                }
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
        public async Task<ActionResult> Create(Customer customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to create the customer.";
                    return View(customer);
                }
            }
        }

        // GET: customer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Customer customer = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                }
                else
                {
                    return HttpNotFound("Customer not found.");
                }
            }

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return new HttpStatusCodeResult(400, "ID mismatch between route and body.");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to update the customer.";
                    return View(customer);
                }
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

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(responseContent);
                }
                else
                {
                    return HttpNotFound("Customer not found.");
                }
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete the customer.";
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }
    }
}
