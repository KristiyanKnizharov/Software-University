using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Repository
{
    public class RepositoryViewModel
    {
        //<th scope = "col" > Name </ th >
        //  < th scope="col">Owner</th>
        //  <th scope = "col" > Created On</th>
        //  <th scope = "col" > Commits Count</th>
        //  <th scope = "col" > Commit </ th >
        public string Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public string CreatedOn { get; set; }

        public int CommitsCount { get; set; }

        public string Commit { get; set; }

    }
}
