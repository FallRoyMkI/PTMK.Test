using PTMK.Models.Dtos;

namespace PTMK.Contracts;

public interface IPersonManager
{
    public void CreateLine(string fullName, DateTime dateOfBirth, string sex);
    public List<PersonInfoResponse> GetAllLinesWithUniqueFullNamesAndData();
    public List<PersonLowInfoResponse> GetAllMalesWithFirstLetterFInFullName();
}