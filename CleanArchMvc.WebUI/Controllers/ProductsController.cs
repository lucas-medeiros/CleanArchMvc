using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }

    [HttpGet()]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            await _productService.AddAsync(productDto);
            return RedirectToAction(nameof(Index));
        }
        return View(productDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);
        if ((id == null) || (productDto == null))
            return NotFound();

        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name", productDto.CategoryId);
        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateAsync(productDto);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(productDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);
        if ((id == null) || (productDto == null))
            return NotFound();

        return View(productDto);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _productService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var productDto = await _productService.GetByIdAsync(id);
        if ((id == null) || (productDto == null))
            return NotFound();

        var image = Path.Combine(_webHostEnvironment.WebRootPath, "images\\" + productDto.Image);
        ViewBag.ImageExists = System.IO.File.Exists(image);
        return View(productDto);
    }
}
