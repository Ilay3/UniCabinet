using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using System;
using System.Threading.Tasks;

namespace UniCabinet.Application.UseCases.ExamUseCase
{
    public class UpdateExamUseCase
    {
        private readonly IExamRepository _examRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;

        public UpdateExamUseCase(IExamRepository examRepository, IDisciplineDetailRepository disciplineDetailRepository)
        {
            _examRepository = examRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
        }

        public async Task<bool> ExecuteAsync(ExamDTO examDTO, ModelStateDictionary modelState)
        {
            var existingExam = await _examRepository.GetExamByIdAsync(examDTO.Id);
            if (existingExam == null)
            {
                modelState.AddModelError("", "Экзамен не найден.");
                return false;
            }

            var disciplineDetail = await _disciplineDetailRepository.GetByIdAsync(examDTO.DisciplineDetailId);
            if (disciplineDetail == null)
            {
                modelState.AddModelError("", "Неверно указана дисциплина.");
                return false;
            }

            if (examDTO.Date.Date < DateTime.Today)
            {
                modelState.AddModelError("", "Дата экзамена не может быть в прошлом.");
                return false;
            }

            await _examRepository.UpdateExamAsync(examDTO);
            return true;
        }

    }
}
