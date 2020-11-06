using Git.Services;
using Git.ViewModels.Commit;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController
            (ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var commitView = new CommitCreateModel
            {
                Id = id,
                Name = repositoriesService.GetNameById(id)
            };


            return this.View(commitView);
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            };

            if (description.Length < 5)
            {
                this.Error("Description must have at least 4 characters.");
            }

            var userId = this.GetUserId();

            this.commitsService.CreateCommit(description, userId, id);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string commitId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.commitsService.DeleteCommit(commitId);

            return this.Redirect("/Commits/All");
        }


        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            var allComments = this.commitsService.GetAllCommits(userId);

            return this.View(allComments);
        }
    }
}
