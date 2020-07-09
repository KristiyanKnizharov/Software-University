using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }
        public int UserId { get; set; }
        public string Username { get; set; }
        //TODO: Password Hasher(MD5 Hash Generator) - default algorithm
        //Hashed password -> 123456789 => 25f9e794323b453885f5181f1b624d0b
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
