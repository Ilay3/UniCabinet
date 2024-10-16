﻿using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Web.Extension.Lecture;
using UniCabinet.Web.Mapping.Lecture;
using UniCabinet.Web.ViewModel.Lecture;

namespace UniCabinet.Web.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly ILectureService _lectureService;
        public LectureController(ILectureRepository lectureRepository, ILectureService lectureService)
        {
            _lectureRepository = lectureRepository;
            _lectureService = lectureService;
        }

        [HttpGet]
        public async Task<IActionResult> LecturesList(int id)
        {
            var lectureListDTO = await _lectureRepository.GetLectureListByDisciplineDetailId(id);

            var lectureListViewModel = await lectureListDTO.GetLectureViewModelAsync();
            
            ViewBag.Discipline = await _lectureService.GetDisciplineById(id);
            ViewBag.DisciplineDetaildId = id;

            return View(lectureListViewModel);
        }

        [HttpGet]
        public IActionResult LectureAddModal(int id)
        {
            ViewBag.DisciplineDetaildId = id;
            return PartialView("_LectureAddModal");
        }

        [HttpPost]
        public async Task<IActionResult> AddLecture(LectureAddViewModel viewModel)
        {
            var lectureDTO = viewModel.GetLectureDTO();
            await _lectureRepository.AddLectureAsync(lectureDTO);

            var disciplineDId = viewModel.DisciplineDetailId;

            return RedirectToAction("LecturesList", new {id = disciplineDId});

        }
    }
}
