using MillionRegister.Core.Domain.Entities;

namespace MillionRegister.Core.Application.Dtos;

public class RecordBatchDto
{
    public List<FixedIncome> Batch { get; set; } = new();
}