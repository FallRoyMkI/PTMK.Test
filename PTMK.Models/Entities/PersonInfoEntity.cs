using System.ComponentModel.DataAnnotations;

namespace PTMK.Models.Entities;
public class PersonInfoEntity
{
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Sex { get; set; }
}