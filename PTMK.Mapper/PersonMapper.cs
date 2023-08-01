using AutoMapper;
using PTMK.Models.Dtos;
using PTMK.Models.Entities;

namespace PTMK.Mapper;
public class PersonMapper
{
    private readonly MapperConfiguration _configuration;

    public PersonMapper()
    {
        _configuration = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<PersonInfoRequest, PersonInfoEntity>();
                cfg.CreateMap<PersonInfoEntity, PersonLowInfoResponse>()
                    .ForMember(d => d.DateOfBirth,
                        o => o.MapFrom
                            (s => DateOnly.FromDateTime(s.DateOfBirth)));
                cfg.CreateMap<PersonInfoEntity, PersonInfoResponse>()
                    .ForMember(d=> d.DateOfBirth,
                        o=> o.MapFrom
                            (s=> DateOnly.FromDateTime(s.DateOfBirth)))
                    .ForMember(d=> d.FullYears, 
                    o => o.MapFrom
                        (s => (DateTime.Today.Date - s.DateOfBirth).Days / 365));
            });
    }

    public PersonInfoEntity MapPersonInfoRequestToPersonInfoEntity(PersonInfoRequest persons)
    {
        return _configuration.CreateMapper().Map<PersonInfoEntity>(persons);
    }

    public List<PersonInfoResponse> MapListPersonInfoEntityToListPersonInfoResponse(List<PersonInfoEntity> persons)
    {
        return _configuration.CreateMapper().Map<List<PersonInfoResponse>>(persons);
    }

    public List<PersonLowInfoResponse> MapListPersonInfoEntityToListPersonLowInfoResponse(List<PersonInfoEntity> persons)
    {
        return _configuration.CreateMapper().Map<List<PersonLowInfoResponse>>(persons);
    }
}