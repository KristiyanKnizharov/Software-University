using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Commit
{
    public class CommitViewModel
    {
        public string Id { get; set; }
        public string NameRepository { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }

    }
}
