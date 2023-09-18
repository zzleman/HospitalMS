using ZeusMed.Core.Interfaces;

namespace ZeusMed.Core.Entities;

public class Service: IEntity
{
    public int Id { get; set; }

    public string? Title { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? ImagePath { get; set; } = null!;

}

