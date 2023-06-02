using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandProfile : Profile
{
    public CreateSegmentCommandProfile()
    {
        CreateMap<Entities.v1.Segment, CreateSegmentCommand>().ReverseMap();
        CreateMap<Entities.v1.Segment, CreateSegmentCommandResult>().ReverseMap();
    }
}