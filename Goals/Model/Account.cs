using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goals.Model
{
    public class Account
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
