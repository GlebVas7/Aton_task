namespace Aton_task.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool KeepLoggetIn { get; set; }
    }
}
