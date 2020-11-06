using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Repository
{
    public class CreateRepositoryModel
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsPublic { get; set; }

        public string OwnerId { get; set; }
    }
}