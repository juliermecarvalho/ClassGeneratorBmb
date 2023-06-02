using AutoMapper;

namespace Bmb.Corporate.Customer.MasterData.Domain.MinhaClasse.Commands.UpdateMinhaClasseCommand.v1;

public class UpdateMinhaClasseCommandProfile : Profile
{
    public UpdateMinhaClasseCommandProfile()
    {
        CreateMap<Entities.v1.MinhaClasse, UpdateMinhaClasseCommand>().ReverseMap();
        CreateMap<Entities.v1.MinhaClasse, UpdateMinhaClasseCommandResult>().ReverseMap();
    }
}
