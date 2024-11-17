using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicForm.DTOs;
using BasicForm.EF;

namespace BasicForm.Controllers
{
    public class ProductController : Controller
    {
        BasicFormEntities1 db = new BasicFormEntities1();

        public static Product Convert(ProductDTO p)
        {
            return new Product()
            {
                ProductId = p.ProductId,
                Name = p.CommercialName + " " + p.CommonName,
                Description = p.Description,
                Price = p.Price
            };
        }

        public static ProductDTO Convert(Product p)
        {
            return new ProductDTO()
            {
                ProductId = p.ProductId,
                CommercialName = p.Name.Split(' ')[0],
                CommonName = p.Name.Split(' ')[1],
                Description = p.Description,
                Price = p.Price
            };
        }

        public static List<ProductDTO> Convert(List<Product> products)
        {
            List<ProductDTO> dtos = new List<ProductDTO>();
            foreach (Product p in products)
            {
                dtos.Add(Convert(p));
            }
            return dtos;
        }

        // GET: Product
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductDTO());
        }

        [HttpPost]
        public ActionResult Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = Convert(productDTO);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            return View(Convert(productDTO));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(productDTO.ProductId);
                product = Convert(productDTO);
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productDTO);
        }
    }
}