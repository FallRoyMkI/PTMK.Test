using PTMK.Models.Entities;

namespace PTMK.Contracts;

public interface IPersonRepository
{
    public void CreateLine(PersonInfoEntity line);
    public List<PersonInfoEntity> GetAllLinesWithUniqueFullNamesAndData();
    public List<PersonInfoEntity> GetAllMalesWithFirstLetterFInFullName();
}