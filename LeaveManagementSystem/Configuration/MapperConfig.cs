using LeaveManagementSystem.Data;
using AutoMapper;

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
