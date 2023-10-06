using X.PagedList;
using ZeusMed.Core.Entities;

namespace ZeusMed.UI.ViewModels
{
    public class ServiceVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Info { get; set; }

        public string ImagePath { get; set; }


        public IPagedList<Service> Services { get; set; } = null!;
    }

}
