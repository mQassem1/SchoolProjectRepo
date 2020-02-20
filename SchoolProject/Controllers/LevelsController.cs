using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Data;
using SchoolProject.Models;
using SchoolProject.ViewModels;

namespace SchoolProject.Controllers
{
    [Authorize]
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
        public IActionResult Create(Level model)
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("LevelNotFound");
            }

            Level level = levelRepository.GetLevel(id.Value);

            if (level == null)
            {
                return View("LevelNotFound");
            }

            Level model = new Level
            {
                LevelId = level.LevelId,
                LevelName = level.LevelName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Level model)
        {
            if (ModelState.IsValid)
            {
                Level level = levelRepository.GetLevel(model.LevelId);

                if (level == null)
                {
                    return View("LevelNotFound");
                }

                level.LevelId = model.LevelId;
                level.LevelName = model.LevelName;

                levelRepository.UpdateLevel(level);
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("LevelNotFound");
            }

            Level level = levelRepository.GetLevel(id.Value);
            if (level == null)
            {
                return View("LevelNotFound");
            }

            return View(level);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            levelRepository.DeleteLevel(id);

            return RedirectToAction("index");
        }

    }
}
