using Git.ViewModels.Repository;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        string Create(CreateRepositoryModel input,string username);
        ICollection<RepositoryViewModel> All();

        void AddRepoToUserCollection(string userId, string repositoryId);

        bool IsRepoNameIsAvailable(string repoName);

        ICollection<RepositoryViewModel> GetAllPublic();

        string GetNameById(string id);
    }
}
