namespace NextPath.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Cnpj { get; set; }
    public string? Industry { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
