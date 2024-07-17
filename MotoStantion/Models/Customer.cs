public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    // Навигационное свойство для связи с мотоциклами
    public ICollection<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
}