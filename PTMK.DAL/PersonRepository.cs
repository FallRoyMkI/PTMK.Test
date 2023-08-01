using Microsoft.EntityFrameworkCore;
using PTMK.Contracts;
using PTMK.Models.Entities;

namespace PTMK.DAL;

public class PersonRepository : IPersonRepository
{
    private readonly Context _context;

    public PersonRepository(Context context)
    {
        _context = context;
    }

    public void CreateLine(PersonInfoEntity line)
    {
        _context.Persons.Add(line);
        _context.SaveChanges();
    }

    public List<PersonInfoEntity> GetAllLinesWithUniqueFullNamesAndData()
    {
        return _context.Persons.FromSql($"Select Id, FullName, DateOfBirth, Sex from (select Persons.*, \r\nrow_number() over (partition by FullName, DateOfBirth order by FullName asc) rn\r\nfrom Persons) t where rn=1").ToList();
    }

    public List<PersonInfoEntity> GetAllMalesWithFirstLetterFInFullName()
    {
        return _context.Persons.FromSql($"Select Id, FullName, DateOfBirth, Sex from dbo.Persons where SUBSTRING(FullName,1,1) =\'F\' and Sex = \'Мужской\'").ToList();
    }
}