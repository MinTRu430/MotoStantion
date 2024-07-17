public class RepairOrder
{
    public int RepairOrderId { get; set; }
    public int? MotorcycleId { get; set; }
    public int? EmployeeId { get; set; }

    private DateTime _orderDate;
    public DateTime OrderDate
    {
        get => _orderDate;
        set => _orderDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    private DateTime? _estimatedCompletionDate;
    public DateTime? EstimatedCompletionDate
    {
        get => _estimatedCompletionDate;
        set => _estimatedCompletionDate = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : (DateTime?)null;
    }

    private DateTime? _actualCompletionDate;
    public DateTime? ActualCompletionDate
    {
        get => _actualCompletionDate;
        set => _actualCompletionDate = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : (DateTime?)null;
    }

    public string Status { get; set; }
    public decimal TotalCost { get; set; }

    public virtual Motorcycle? Motorcycle { get; set; }
    public virtual Employee? Employee { get; set; }
}