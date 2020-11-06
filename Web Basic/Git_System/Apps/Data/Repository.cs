﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Git.Data
{
    public class Repository
    {
        public Repository()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Commits = new HashSet<Commit>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }


        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<Commit> Commits { get; set; }
    }
}