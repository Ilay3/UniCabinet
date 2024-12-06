using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniCabinet.Application.UseCases.ExamUseCase
{
    public class SaveExamResultsUseCase
    {
        private readonly IExamResultRepository _examResultRepository;
        private readonly IExamRepository _examRepository;

        public SaveExamResultsUseCase(IExamResultRepository examResultRepository, IExamRepository examRepository)
        {
            _examResultRepository = examResultRepository;
            _examRepository = examRepository;
        }

        public async Task<bool> ExecuteAsync(List<ExamResultDTO> examResults, ModelStateDictionary modelState)
        {
            if (examResults == null || !examResults.Any())
            {
                modelState.AddModelError("", "Нет данных для сохранения оценок.");
                return false;
            }

            int examId = examResults.First().ExamId;
            var exam = await _examRepository.GetExamByIdAsync(examId);
            if (exam == null)
            {
                modelState.AddModelError("", "Экзамен не найден. Невозможно сохранить результаты.");
                return false;
            }


            foreach (var result in examResults)
            {
                await _examResultRepository.AddOrUpdateExamResultAsync(result);
            }

            return true;
        }
    }
}
