using AutoMapper;
using TreinamentoAPI.DTOs;
using TreinamentoAPI.Entidades;

namespace TreinamentoAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<ProdutoDto, Produto>();

            CreateMap<Funcionario, FuncionarioDto>();
            CreateMap<FuncionarioDto, Funcionario>();
        }
    }
}
