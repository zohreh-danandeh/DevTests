namespace SchedulingSys.Models;

public class Employees
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required Availability Availability { get; set; }
}


public class Availability
{
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
}