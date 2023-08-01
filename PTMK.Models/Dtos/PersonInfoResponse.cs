namespace PTMK.Models.Dtos;

public class PersonInfoResponse
{
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Sex { get; set; }
    public int FullYears { get; set; }
}