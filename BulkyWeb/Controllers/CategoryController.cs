
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString()) {
            //    ModelState.AddModelError("name", "the DisplayOrder cannont be exactly match the Name");
            //}

            if (ModelState.IsValid) {
                categoryRepo.Add(obj);
                categoryRepo.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("index"); 
            }
                  
            return View();  
        }

        public IActionResult Edit(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = categoryRepo.Get(u=>u.Id==id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                categoryRepo.Update(obj);
                categoryRepo.Save();
                TempData["success"] = "Category Updated Successfully";

                return RedirectToAction("index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = categoryRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = categoryRepo.Get(u => u.Id == id);

            if (obj == null) { 
                return NotFound();
            }
            categoryRepo.Remove(obj);
            categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction("index");

        }
    }
}
