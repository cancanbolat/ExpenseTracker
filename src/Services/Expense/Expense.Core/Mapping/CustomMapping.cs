using AutoMapper;

namespace Expense.Core.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            //Record
            CreateMap<Models.Record, DTOs.RecordDto>().ReverseMap();
            CreateMap<Models.Record, CQRS.Record.Requests.CreateRecordRequest>().ReverseMap();

            //Category
            CreateMap<Models.Category, DTOs.CategoryDto>().ReverseMap();
        }
    }
}
