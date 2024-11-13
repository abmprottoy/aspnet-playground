using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventroyManagement.DTOs;
using InventroyManagement.EF;

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
                Category = p.Category.ToString(),
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
                Category = (ProductCategory)Enum.Parse(typeof(ProductCategory), p.Category)
            };
        }
        public static List<ProductDTO> Convert(List<Product> data)
        {
            var list = new List<ProductDTO>();
            foreach (var p in data)
            {
                list.Add(Convert(p));
            }
            return list;
        }

        // GET: Product
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(Convert(data));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDTO p)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(Convert(p));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);

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
            return View(productDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                // Find the existing product
                var product = db.Products.Find(productDTO.ProductID);
                if (product == null)
                {
                    return HttpNotFound();
                }

                // Update product properties
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.StockQuantity = productDTO.StockQuantity;
                product.Category = productDTO.Category.ToString();

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