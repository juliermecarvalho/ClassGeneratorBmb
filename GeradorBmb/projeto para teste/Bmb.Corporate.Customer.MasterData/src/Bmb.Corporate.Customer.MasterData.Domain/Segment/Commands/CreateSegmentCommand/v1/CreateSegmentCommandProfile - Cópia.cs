using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.CreateSegmentCommand.v1;

public class CreateSegmentCommandProfile : Profile
{
    public CreateSegmentCommandProfile()
    {
        CreateMap<CreateSegmentCommand, Entities.v1.Segment>();

      
    }
}