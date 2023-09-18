using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities;

public class Doctor : IEntity
{
    public int Id { get; set; }

    public string? Fullname { get; set; } = null!;

    public string? Department { get; set; } = null!;

    public string? ImagePath { get; set; } = null!;

}

