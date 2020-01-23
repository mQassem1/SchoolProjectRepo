using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    public class LevelsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly ILevelRepository levelRepository;
        private readonly ILogger<LevelsController> logger;

        public LevelsController(ApplicationDbContext context,
                                ILevelRepository levelRepository,
                                ILogger<LevelsController> logger)
        {
            this.context = context;
            this.levelRepository = levelRepository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var levels = levelRepository.GetAllLevels().ToList();
            return  View(levels);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("LevelNotFound");
            }

            var level = levelRepository.GetLevel(id.Value);
            if (level == null)
            {
                return View("LevelNotFound");
            }

            return View(level);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateLevelViewModel model)
        {
            if (ModelState.IsValid)
            {
                Level level = new Level
                {
                    LevelName = model.LevelName
                };

                levelRepository.AddLevel(level);

                return RedirectToAction("Index");

            }
            
            return View(model);
        }

        // GET: Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var level = await context.Levels.FindAsync(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // POST: Levels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LevelId,LevelName")] Level level)
        {
            if (id != level.LevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(level);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LevelExists(level.LevelId))
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
            return View(level);
        }

        // GET: Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var level = await context.Levels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (level == null)
            {
                return NotFound();
            }

            return View(level);
        }

        // POST: Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var level = await context.Levels.FindAsync(id);
            context.Levels.Remove(level);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LevelExists(int id)
        {
            return context.Levels.Any(e => e.LevelId == id);
        }
    }
}
