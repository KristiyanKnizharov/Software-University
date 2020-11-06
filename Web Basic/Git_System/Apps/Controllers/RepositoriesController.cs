using Git.Services;
using Git.ViewModels.Repository;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            //Guest and Users can access /Repositories/All
            if (!this.IsUserSignedIn())
            {
                var repos = this.repositoriesService.GetAllPublic();
                return this.View(repos);
            }
            var all = this.repositoriesService.All();

            return this.View(all);
        }

        
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string isPublic)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.Error("Name is required!");
            }
            var isItPublic = isPublic == "Public" ? true : false;
            var createRepo = new CreateRepositoryModel
            {

                Name = name,
                IsPublic = isItPublic
            };

            var repoId = this.repositoriesService.Create(createRepo, this.GetUserId());

            return this.Redirect("/Repositories/All");
        }

    }
}
