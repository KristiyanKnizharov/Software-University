using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.Data;
using Git.ViewModels.Repository;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(CreateRepositoryModel input, string username)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == username);

            var repo = new Repository
            {
                Name = input.Name,
                CreatedOn = DateTime.UtcNow.ToString(),
                IsPublic = input.IsPublic,
                OwnerId = user.Id
            };

            this.db.Repositories.Add(repo);
            this.db.SaveChanges();
            return repo.Id;

        }

        public void AddRepoToUserCollection(string userId, string repositoryId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var repo = this.db.Repositories.FirstOrDefault(x => x.Id == repositoryId);

        }

        public ICollection<RepositoryViewModel> All()
        {
            var all = this.db.Repositories.Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username.ToString(),
                CreatedOn = x.CreatedOn,
                CommitsCount = x.Commits.Count(),
                Commit = x.Commits.FirstOrDefault(c => c.Creator.Username == x.Name).Description
            }).ToList();

            return all;
        }
        public string GetNameById(string id)
        {
            return this.db.Repositories
                .Where(x => x.Id == id)
                .Select(r => r.Name).FirstOrDefault();
        }
        public bool IsRepoNameIsAvailable(string repoName)
        {
            return !this.db.Repositories.Any(c => c.Name == repoName);
        }

        public ICollection<RepositoryViewModel> GetAllPublic()
        {
            return this.db.Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn,
                    CommitsCount = x.Commits.Count(),
                    Commit = x.Commits.ToString()
                })
                .ToArray();
        }
    }
}
