using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventroyManagement.DTOs;
using InventroyManagement.Models;
using InventroyManagement.EF;
using InventroyManagement.Helpers;
using System.Data.Entity;

namespace InventroyManagement.Controllers
{
    public class ProductController : Controller
    {
        InventoryManagementEntities db = new InventoryManagementEntities();

        public static Product Convert(ProductDTO p)
        {
            return new Product()
            {
                ProductId = p.ProductID,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category
            };
        }
        public static ProductDTO Convert(Product p)
        {

            return new ProductDTO()
            {
                ProductID = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price ?? 0,
                StockQuantity = p.StockQuantity ?? 0,
                Category = p.Category
            };
        }
        public static List<ProductDTO> Convert(List<Product> data)
        {
            return data.Select(Convert).ToList();
        }

        // GET: Product
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(Convert(data));
        }

        // GET: Product/Create (Create Product)
        public ActionResult Create()
        {
            var productDto = new ProductDTO();
            ViewBag.ProductCategories = ProductCategories.Categories;
            return View(productDto);
        }

        // POST: Product/Create (Save New Product)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDTO productDTO)
        {
            ViewBag.ProductCategories = ProductCategories.Categories;

            if (ModelState.IsValid)
            {
                // Convert ProductDTO to Product and add to db
                var product = Convert(productDTO);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDTO);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productDTO = Convert(product);
            return View(productDTO);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productDTO = Convert(product);
            ViewBag.ProductCategories = ProductCategories.Categories;
            return View(productDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                // Find the existing product in the database
                var product = db.Products.Find(productDTO.ProductID);
                if (product == null)
                {
                    return HttpNotFound();
                }

                // Update product fields with the data from the DTO
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.StockQuantity = productDTO.StockQuantity;
                product.Category = productDTO.Category;

                // Mark the product entity as modified
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDTO);
        }

        // GET: Product/Delete/5 (Delete Product)
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var productDTO = Convert(product);
            return View(productDTO);
        }

        // POST: Product/Delete/5 (Confirm Delete)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}