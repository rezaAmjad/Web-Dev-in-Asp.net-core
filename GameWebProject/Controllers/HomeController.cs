using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameWebProject.Models.SaleModel;
using GameWebProject.Models.UserModel;
using GameWebProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameWebProject.Controllers
{
    public class HomeController : Controller
    {
        //to access the items list 
        private readonly IItemRepository _itemRepository;
        //information about the web hosting environment the application is running in
        private readonly IHostingEnvironment hostingEnvironment;
        //to access user information
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IItemRepository itemRepository, IHostingEnvironment hostingEnvironment, 
            UserManager<ApplicationUser> userManager)
        {
            _itemRepository = itemRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        
        /*
         * this function will either return the whole list or the result of the search
         */
        public ViewResult Index(string search)
        {

            if (!string.IsNullOrEmpty(search))
            {
                var items_found = _itemRepository.SearchItems(search);
                return View(items_found);
            }
            var model = _itemRepository.GetItems();
            return View(model);
        }

        /*
         * this method will return a view of the selected post
         */
        public ViewResult Details(int id)
        {
            Item item = _itemRepository.GetItem(id);
            if(item == null)
            {
                Response.StatusCode = 404;
                return View("PageNotFound");
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Item = item,
                PageTitle = "Item Details"
        };
            return View(homeDetailsViewModel);
        }

        

        /*
         * this method will return the page to edit a post
         */
        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Item item = _itemRepository.GetItem(id);
            ItemEditViewModel itemEditViewModel = new ItemEditViewModel
            {
                Id = item.Id,
                ProductName = item.ProductName,
                Category = item.Category,
                Platform = item.Platform,
                Condition = item.Condition,
                LookingFor = item.LookingFor,
                ExistingPhotoPath = item.PhotoPath
            };
            return View(itemEditViewModel);
        } 

        /*
         * this method will return the edited post
         */
        [HttpPost]
        [Authorize]
        public IActionResult Edit(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                Item item = _itemRepository.GetItem(model.Id);
                item.ProductName = model.ProductName;
                item.Category = model.Category;
                item.Platform = model.Platform;
                item.Condition = model.Condition;
                item.LookingFor = model.LookingFor;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    item.PhotoPath = ProcessUploadedFile(model);
                }
                _itemRepository.Update(item);
                return RedirectToAction("details", new { id = item.Id });
            }
            return View();
        }

        /*
         * to delete a post
         */
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Item item = _itemRepository.GetItem(id);
            if (item == null)
            {
                ViewBag.ErrorMessage = $"Product cannot be found";
                return View("NotFound");
            }
            _itemRepository.Delete(id);
            return RedirectToAction("Index","home");  
        }

        /*
         * this method will respond to get request and return the edit view 
         */
        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        /*
         * this method will create a new post 
         * and redirect the user to that post after creation.
         */
        [HttpPost]
        [Authorize]
        public IActionResult Create(ItemCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user_id = userManager.GetUserId(HttpContext.User);
                string uniqueFileName = ProcessUploadedFile(model);
                Item newItem = new Item
                {
                    userId = user_id,
                    ProductName = model.ProductName,
                    Category = model.Category,
                    Platform = model.Platform,
                    Condition = model.Condition,
                    LookingFor = model.LookingFor,

                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath =  uniqueFileName
                };
                _itemRepository.Add(newItem);
                return RedirectToAction("details", new { id = newItem.Id });
            }
            return View();
        }

        
        /*
                private string ProcessUploadedFiles(ItemCreateViewModel model)
                {
                    string uniqueFileName = null;
                    // If the Photo property on the incoming model object is not null, then the user
                    // has at least one photo to upload.
                    if (model.Photos != null && model.Photos.Count > 0)
                    {
                        foreach (FormFile photo in model.Photos)
                        {
                            // The image must be uploaded to the images folder in wwwroot
                            // To get the path of the wwwroot folder we are using the inject
                            // HostingEnvironment service provided by ASP.NET Core
                            string UploadPath = Path.Combine(hostingEnvironment.WebRootPath, "images");

                            // To make sure the file name is unique we are appending a new
                            // GUID value and and an underscore to the file name
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(UploadPath, uniqueFileName);

                            using(var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                // Use CopyTo() method provided by IFormFile interface to
                                // copy the file to wwwroot/images folder
                                photo.CopyTo(fileStream);
                            }

                        }
                    }

                    return uniqueFileName;
                }*/
/*
 * this code is to upload file to the database 
 * and it is common between create and edit 
 */
        private string ProcessUploadedFile(ItemCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
