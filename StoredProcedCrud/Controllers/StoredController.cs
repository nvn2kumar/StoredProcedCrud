using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoredProcedCrud.Data;
using StoredProcedCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedCrud.Controllers
{
    public class StoredController : Controller
    {
        private readonly ApplicationDbContext context;

        public StoredController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            //var data = context.Vendors.FromSqlRaw("spGetAllwendorList").ToList();
            //return View(data);
            return View();
        }

        public JsonResult GetAlldata()
        {
            var data = context.Vendors.FromSqlRaw("spGetAllwendorList").ToList();
            return new JsonResult(data);
        }
        //public IActionResult Create()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Vendor vendor)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //context.Database.ExecuteSqlRaw($"spInsert_Wendor '{vendor.Name}','{vendor.Gender}','{vendor.City}',{vendor.Pincode}");
        //            //context.Database.ExecuteSqlRaw($"spInsert_Wendor @name='{vendor.Name}',@gender='{vendor.Gender}',@city='{vendor.City}',@pincode={vendor.Pincode}");
        //            string parameter = $"spInsert_Wendor @name='{vendor.Name}',@gender='{vendor.Gender}',@city='{vendor.City}',@pincode={vendor.Pincode}";
        //            context.Database.ExecuteSqlRaw(parameter);
        //            TempData["success"]= "Record is successfully inserted";
        //            return RedirectToAction("Index");
        //        }
        //        ModelState.AddModelError(string.Empty, "Model State is Not valid!");
        //    }
        //    catch (Exception ex)
        //    {

        //        ModelState.AddModelError(string.Empty, ex.Message);
        //    }
        //    return View();
        //}

        public IActionResult Delete(int Id)
        {
           
            if (Id==0)
            {
                return NotFound();
            }

            context.Database.ExecuteSqlRaw($"spDelete_Wendor @id={Id}");
            TempData["success"] = "Record is successfully Deleted";
            return RedirectToAction("Index");
                
            
        }

        public IActionResult Edit(int Id)
        {
            Vendor objVendor = new Vendor();
            try
            {
                if (Id==0)
                {
                    return NotFound();
                }

                var data = context.Vendors.FromSqlRaw($"spGetDetailById '{Id}'");
                
                foreach(var item in data)
                {
                    objVendor.Id = item.Id;
                    objVendor.Name = item.Name;
                    objVendor.Gender = item.Gender;
                    objVendor.Pincode = item.Pincode;
                    objVendor.City = item.City;
                }
                //return View(objVendor);
                return View("Saveorupdate", objVendor);
                
            }
            catch (Exception)
            {

                return View("Saveorupdate", objVendor);
            }
            
        }
        [HttpPost]
        public IActionResult Edit(Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Database.ExecuteSqlRaw($"spUpdate_Wendor {vendor.Id},'{vendor.Name}','{vendor.Gender}','{vendor.City}',{vendor.Pincode}");
                    TempData["success"] = "Record is successfully Updated";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Model State is Not valid!");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult Saveorupdate()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Saveorupdate(Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Database.ExecuteSqlRaw($"spSaveorUpdate_Wendor {vendor.Id},'{vendor.Name}','{vendor.Gender}','{vendor.City}',{vendor.Pincode}");
                    if (vendor.Id!=0)
                    {
                        TempData["success"] = "Record is successfully Updated !";
                    }
                    else
                    {
                        TempData["success"] = "Record is successfully Saved !";
                    }
                    
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Model State is Not valid!");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
