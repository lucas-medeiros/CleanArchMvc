using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CategoryDTO categoryDto)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }
        return View(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var categoryDTO = await _categoryService.GetByIdAsync(id);
        if ((id == null) || (categoryDTO == null))
            return NotFound();

        return View(categoryDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoryDTO categoryDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.UpdateAsync(categoryDto);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoryDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        var categoryDTO = await _categoryService.GetByIdAsync(id);
        if ((id == null) || (categoryDTO == null))
            return NotFound();

        return View(categoryDTO);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _categoryService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var categoryDTO = await _categoryService.GetByIdAsync(id);
        if ((id == null) || (categoryDTO == null))
            return NotFound();

        return View(categoryDTO);
    }
}