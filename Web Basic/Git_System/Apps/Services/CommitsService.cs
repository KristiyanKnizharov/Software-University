using Git.Data;
using Git.ViewModels.Commit;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService  : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public string CreateCommit(string description,string creatorId,string repoId)
        {
            var newCommit = new Commit
            {
                Id = Guid.NewGuid().ToString(),
                Description = description,
                CreateOn = DateTime.Now.ToString(),
                CreatorId = creatorId,
                RepositoryId = repoId
            };
            this.db.Commits.Add(newCommit);
            this.db.SaveChanges();

            return newCommit.Id;
        }

        
        public void DeleteCommit(string id)
        {
            var currCommit = this.db.Commits
                .FirstOrDefault(x => x.Id == id);

            this.db.Commits.Remove(currCommit);
            this.db.SaveChanges();
        }

        public ICollection<CommitViewModel> GetAllCommits(string id)
        {
            var allCommits = this.db.Commits
                .Where(c => c.CreatorId == id)
                .Select(c => new CommitViewModel
                {
                    Id = c.Id,
                    NameRepository = c.Repository.Name,
                    CreatedOn = c.CreateOn,
                    Description = c.Description

                }).ToArray();

            return allCommits;
        }
    }
}
