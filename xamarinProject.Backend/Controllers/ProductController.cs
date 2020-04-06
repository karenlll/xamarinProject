using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xamarinProject.Backend.Helpers;
using xamarinProject.Backend.Models;
using xamarinProject.Common.Models;

namespace xamarinProject.Backend.Controllers
{
    public class ProductController : Controller
    {
        private readonly LocalDataContext _context;
        private readonly FilesHelper _filesHelper;

        public ProductController(LocalDataContext context, FilesHelper helper)
        {
            _context = context;
            _filesHelper = helper;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.OrderBy(p => p.Description).ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductView view)
        {
            Product product = this.ToProduct(view, "");
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                pic = await savePhotoAsync(view.ImageFile);

                product = this.ToProduct(view, pic);

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Product ToProduct(ProductView view, string pic)
        {
            return new Product
            {
                Description = view.Description,
                ImagePath = pic,
                IsAvailable = view.IsAvailable,
                Price = view.Price,
                PublishOn = view.PublishOn,
                Remarks = view.Remarks,
                ProductId = view.ProductId,
                FromWeb = true,
            };
        }

        private ProductView ToProductView(Product view)
        {
            return new ProductView
            {
                Description = view.Description,
                ImagePath = view.ImagePath,
                ImageFile = null,
                IsAvailable = view.IsAvailable,
                Price = view.Price,
                PublishOn = view.PublishOn,
                Remarks = view.Remarks,
                ProductId = view.ProductId
            };
        }

        private async Task<string> savePhotoAsync(IFormFile image)
        {
            var pic = string.Empty;
            var folder = "wwwroot/lib/bootstrap/Content/Products";
            var relativeFolder = "lib/bootstrap/Content/Products";

            if (image != null)
            {
                pic = await _filesHelper.UploadPhotoAsync(image, folder);
                pic = $"{relativeFolder}/{pic}";
            }

            return pic;
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProductView(product);
            return View(view);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductView view)
        {
            if (id != view.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pic = string.Empty;
                    pic = await savePhotoAsync(view.ImageFile);

                    var product = this.ToProduct(view, pic);

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(view.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
