using AutoMapper;

namespace Bmb.Teste.Operation.MasterData.Domain.Example.Commands.CreateExampleCommand.v1;

public class CreateExampleCommandProfile : Profile
{
    public CreateExampleCommandProfile()
    {
        CreateMap<CreateExampleCommand, Entities.v1.Example>()
            .ForMember(destination => destination.PropertyOne,
                opt => opt.MapFrom(source => source.PropertyOne))
            .ForMember(destination => destination.PropertyTwo,
                opt => opt.MapFrom(source => source.PropertyTwo));
    }
}