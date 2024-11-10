namespace UniCabinet.Domain.Entities;

public class SpecialtyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserEntity> Teachers { get; set; }
}
