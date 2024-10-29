using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Domain.DTO;

namespace UniCabinet.Application.Services
{
    public class PracticalService : IPracticalService
    {
        private readonly IPracticalRepository _practicalRepository;
        private readonly IDisciplineDetailRepository _disciplineDetailRepository;
        private readonly ILogger<PracticalService> _logger;

        public PracticalService(
            IPracticalRepository practicalRepository,
            IDisciplineDetailRepository disciplineDetailRepository,
            ILogger<PracticalService> logger)
        {
            _practicalRepository = practicalRepository;
            _disciplineDetailRepository = disciplineDetailRepository;
            _logger = logger;
        }

        public async Task<List<PracticalDTO>> GetPracticalsByDisciplineDetailIdAsync(int disciplineDetailId)
        {
            return await _practicalRepository.GetPracticalsByDisciplineDetailIdAsync(disciplineDetailId);
        }

        public async Task<PracticalDTO> GetPracticalByIdAsync(int id)
        {
            return await _practicalRepository.GetPracticalByIdAsync(id);
        }

        public async Task AddPracticalAsync(PracticalDTO practicalDTO)
        {
            var disciplineDetail = await _disciplineDetailRepository.GetDisciplineDetailByIdAsync(practicalDTO.DisciplineDetailId);
            if (disciplineDetail == null)
            {
                _logger.LogError($"DisciplineDetail с ID {practicalDTO.DisciplineDetailId} не найден.");
                throw new ArgumentException("DisciplineDetail не найден.");
            }

            // Проверка на достижение лимита практических работ
            var existingPracticals = await _practicalRepository.GetPracticalsByDisciplineDetailIdAsync(practicalDTO.DisciplineDetailId);
            if (existingPracticals.Count >= disciplineDetail.PracticalCount)
            {
                _logger.LogWarning("Достигнуто максимальное количество практических работ для дисциплины ID {DisciplineDetailId}.", disciplineDetail.Id);
                throw new InvalidOperationException("Достигнуто максимальное количество практических работ для этой дисциплины.");
            }

            await _practicalRepository.AddPracticalAsync(practicalDTO);
            _logger.LogInformation($"Практическая работа №{practicalDTO.PracticalNumber} добавлена для DisciplineDetail ID {disciplineDetail.Id}.");
        }

        public async Task UpdatePracticalAsync(PracticalDTO practicalDTO)
        {
            var existingPractical = await _practicalRepository.GetPracticalByIdAsync(practicalDTO.Id);
            if (existingPractical == null)
            {
                _logger.LogError($"Практическая работа с ID {practicalDTO.Id} не найдена.");
                throw new ArgumentException("Практическая работа не найдена.");
            }

            await _practicalRepository.UpdatePracticalAsync(practicalDTO);
            _logger.LogInformation($"Практическая работа №{practicalDTO.PracticalNumber} обновлена для DisciplineDetail ID {existingPractical.DisciplineDetailId}.");
        }

        public async Task DeletePracticalAsync(int id)
        {
            var existingPractical = await _practicalRepository.GetPracticalByIdAsync(id);
            if (existingPractical == null)
            {
                _logger.LogError($"Практическая работа с ID {id} не найдена.");
                throw new ArgumentException("Практическая работа не найдена.");
            }

            await _practicalRepository.DeletePracticalAsync(id);
            _logger.LogInformation($"Практическая работа с ID {id} удалена.");
        }
    }
}
