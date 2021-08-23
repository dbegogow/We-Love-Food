using AutoMapper;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Waiters;

namespace WeLoveFood.Web.Web.Infrastructure.Mapping
{
    public class WaitersProfile : Profile
    {
        public WaitersProfile()
        {
            this.CreateMap<Waiter, ManagerWaiterServiceModel>();
        }
    }
}
