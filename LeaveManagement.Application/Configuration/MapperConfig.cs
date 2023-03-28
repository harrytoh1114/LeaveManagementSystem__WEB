using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using AutoMapper;

namespace LeaveManagement.Application.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<Employee, EmployeeListVM>().ReverseMap();
            CreateMap<Employee, EmployeeAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestCreateVM>().ReverseMap();
        }
    }
}
