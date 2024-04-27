namespace TODOList.TodoCore.Models.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsCompleted { get; set; }
}