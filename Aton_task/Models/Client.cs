namespace Aton_task.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string INN { get; set; }
        public string? ResponsibleFullName { get; set; }
        public string? Status { get; set; }
    }
}
