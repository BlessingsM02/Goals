using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Goals.Model
{
    public class Goal
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public int UserId { get; set; }
        public string GoalName { get; set; }
        public double GoalAmount { get; set; }
        public double CurrentAmount { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAT { get; set; }
        public bool IsAchieved { get; set; }
    }
}