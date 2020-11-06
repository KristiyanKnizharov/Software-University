using Git.ViewModels.Commit;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {
        ICollection<CommitViewModel> GetAllCommits(string id);

        string CreateCommit(string description, string userId, string id);

        void DeleteCommit(string id);

    }
}
