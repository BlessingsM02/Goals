using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goals.Model
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public int AccountId { get; set; }
        public string TransType { get; set; }
        public double Amount { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime MadeAt { get; set; }
    }
}
