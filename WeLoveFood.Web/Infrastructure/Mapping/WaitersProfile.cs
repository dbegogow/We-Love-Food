using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Waiters;

namespace WeLoveFood.Web.Infrastructure.Mapping
{
    public class WaitersProfile : Profile
    {
        public WaitersProfile()
        {
            this.CreateMap<Waiter, ManagerWaiterServiceModel>();
        }
    }
}
