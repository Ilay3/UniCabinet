﻿using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class DisciplineRepositoryImpl : IDisciplineRepository
{
    private readonly ApplicationDbContext _context;

    public DisciplineRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public DisciplineDTO GetDisciplineById(int id)
    {
        var disciplineEntity = _context.Disciplines.Find(id);
        if (disciplineEntity == null) return null;

        return new DisciplineDTO
        {
            Name = disciplineEntity.Name,
            Description = disciplineEntity.Description,
        };
    }

    public List<DisciplineDTO> GetAllDisciplines()
    {
        var disciplineEntity = _context.Disciplines.ToList();

        return disciplineEntity.Select(d => new DisciplineDTO
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
        }).ToList();
    }

    public void AddDiscipline(DisciplineDTO disciplineDTO)
    {
        var disciplineEntity = new DisciplineEntity
        {
            Name = disciplineDTO.Name,
            Description = disciplineDTO.Description,
        };

        _context.Disciplines.Add(disciplineEntity);
        _context.SaveChanges();
    }

    public void DeleteDiscipline(int id)
    {
        var disciplineEntity = _context.Disciplines.Find(id);
        if (disciplineEntity != null)
        {
            _context.Disciplines.Remove(disciplineEntity);
            _context.SaveChanges();
        }
    }

    public void UpdateDiscipline(DisciplineDTO disciplineDTO)
    {
        var disciplineEntity = _context.Disciplines.FirstOrDefault(d => d.Id == disciplineDTO.Id);
        if (disciplineEntity == null) return;

        disciplineEntity.Name = disciplineDTO.Name;
        disciplineEntity.Description = disciplineDTO.Description;

        _context.Disciplines.Update(disciplineEntity);
        _context.SaveChanges();
    }
}