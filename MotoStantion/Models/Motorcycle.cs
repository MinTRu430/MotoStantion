public class Motorcycle
{
    public int MotorcycleId { get; set; }
    public int? CustomerId { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Vin { get; set; } = string.Empty;
    public byte[]? Image { get; set; }

    // Навигационное свойство для связи с клиентом
    public virtual Customer? Customer { get; set; }
}
