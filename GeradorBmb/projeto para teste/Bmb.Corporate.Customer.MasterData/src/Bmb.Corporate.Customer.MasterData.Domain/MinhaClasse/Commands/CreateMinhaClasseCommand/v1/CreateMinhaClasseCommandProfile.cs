using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.CreateMinhaClasseCommand.v1;

public class CreateMinhaClasseCommandProfile : Profile
{
    public CreateMinhaClasseCommandProfile()
    {
        CreateMap<Entities.v1.MinhaClasse, CreateMinhaClasseCommand>().ReverseMap();
        CreateMap<Entities.v1.MinhaClasse, CreateMinhaClasseCommandResult>().ReverseMap();
    }
}
