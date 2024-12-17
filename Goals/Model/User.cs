using SQLite;

namespace Goals.Model
{
    [Table("user")]
    public class User
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
