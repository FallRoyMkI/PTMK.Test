using AutoMapper;
using PTMK.Contracts;
using PTMK.Mapper;
using PTMK.Models.Dtos;
using PTMK.Models.Entities;

namespace PTMK.BLL
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _personRepository;
        private readonly PersonMapper _mapper;

        public PersonManager(IPersonRepository personRepository, PersonMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }


        public void CreateLine(string fullName, DateTime dateOfBirth, string sex)
        {
            PersonInfoRequest dto = new()
            {
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                Sex = sex
            };

            PersonInfoEntity entity = _mapper.MapPersonInfoRequestToPersonInfoEntity(dto);
            _personRepository.CreateLine(entity);
        }

        public List<PersonInfoResponse> GetAllLinesWithUniqueFullNamesAndData()
        {
            List<PersonInfoEntity> response = _personRepository.GetAllLinesWithUniqueFullNamesAndData();
            List<PersonInfoResponse> result = _mapper.MapListPersonInfoEntityToListPersonInfoResponse(response);

            return result;
        }

        public List<PersonLowInfoResponse> GetAllMalesWithFirstLetterFInFullName()
        {
            List<PersonInfoEntity> response = _personRepository.GetAllMalesWithFirstLetterFInFullName();
            List<PersonLowInfoResponse> result = _mapper.MapListPersonInfoEntityToListPersonLowInfoResponse(response);
            
            return result;
        }
    }
}