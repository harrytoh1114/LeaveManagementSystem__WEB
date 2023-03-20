using AutoMapper;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Models.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
        }
    }
}
