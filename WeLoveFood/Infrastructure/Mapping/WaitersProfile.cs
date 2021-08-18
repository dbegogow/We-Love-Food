using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Waiters;

namespace WeLoveFood.Infrastructure.Mapping
{
    public class WaitersProfile : Profile
    {
        public WaitersProfile()
        {
            this.CreateMap<Waiter, ManagerWaiterServiceModel>();
        }
    }
}
