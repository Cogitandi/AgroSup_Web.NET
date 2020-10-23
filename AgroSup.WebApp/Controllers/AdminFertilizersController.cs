using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.WebApp.ViewModels.AdminPanel.Fertilizers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.Controllers
{
    [RequestFormLimits(ValueCountLimit = 5000)]
    [Authorize(Roles = "Administrator")]
    public class AdminFertilizersController : Controller
    {
        private readonly IFertilizerRepository _fertilizerRepository;

        public AdminFertilizersController(IFertilizerRepository fertilizerRepository)
        {
            _fertilizerRepository = fertilizerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var fertilizers = await _fertilizerRepository.GetAll();
            var model = fertilizers.Select(x => new FertilizerViewModel
            {
                Id = x.Id,
                Name = x.Name,
                N = x.N,
                P = x.P,
                K = x.K,
                Ca = x.Ca,
                Mg = x.Mg,
                S = x.S,
                Na = x.Na,
                Delete = false
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(IEnumerable<FertilizerViewModel> fertilizers)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Błąd: Wystąpił problem. Nie wprowadzono zmian";
                return View(fertilizers);
            }

            var FertilizersToSave = fertilizers.Select(x => new Fertilizer
            {
                Id = x.Id,
                Name = x.Name,
                N = x.N,
                P = x.P,
                K = x.K,
                Ca = x.Ca,
                Mg = x.Mg,
                S = x.S,
                Na = x.Na,
            });


            foreach (var item in FertilizersToSave)
            {
                await _fertilizerRepository.Update(item);
            }

            var fertilizersToDelete = fertilizers.Where(x => x.Delete == true);

            foreach (var item in fertilizersToDelete)
            {
                var fertilizer = await _fertilizerRepository.GetById(item.Id);
                await _fertilizerRepository.Delete(fertilizer);
            }
            TempData["message"] = "Zmiany zapisano pomyślnie!";
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FertilizerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var fertilizer = new Fertilizer
            {
                Id = model.Id,
                Name = model.Name,
                N = model.N,
                P = model.P,
                K = model.K,
                Ca = model.Ca,
                Mg = model.Mg,
                S = model.S,
                Na = model.Na,
            };
            await _fertilizerRepository.Add(fertilizer);
            TempData["message"] = "Dodano nowy nawóz!";
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task Update(FertilizerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            var fertilizer = await _fertilizerRepository.GetById(model.Id);
            fertilizer.Id = model.Id;
                fertilizer.Name = model.Name;
                fertilizer.N = model.N;
                fertilizer.P = model.P;
                fertilizer.K = model.K;
                fertilizer.Ca = model.Ca;
                fertilizer.Mg = model.Mg;
            fertilizer.S = model.S;
                fertilizer.Na = model.Na;
            await _fertilizerRepository.Update(fertilizer);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var fertilizer = await _fertilizerRepository.GetById(id);
            try
            {
                await _fertilizerRepository.Delete(fertilizer);
                
            } catch (DbUpdateException)
            {
                TempData["message"] = "Błąd: Nawóz jest wpisany do ewidencji przez użytkowników!";
            }
            
            return RedirectToAction("Index");
        }

    }
}
