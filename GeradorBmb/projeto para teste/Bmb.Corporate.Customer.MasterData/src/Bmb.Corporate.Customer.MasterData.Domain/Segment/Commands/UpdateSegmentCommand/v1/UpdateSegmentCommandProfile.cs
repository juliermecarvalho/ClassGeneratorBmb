using AutoMapper;


namespace Bmb.Corporate.Customer.MasterData.Domain.Segment.Commands.UpdateSegmentCommand.v1
{
    public class UpdateSegmentCommandProfile : Profile
    {
        public UpdateSegmentCommandProfile()
        {
            CreateMap<Entities.v1.Segment, UpdateSegmentCommand>().ReverseMap();
            CreateMap<Entities.v1.Segment, UpdateSegmentCommandResult>().ReverseMap();
        }
    }
}
