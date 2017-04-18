using System.ComponentModel.DataAnnotations;

public partial class MasterScheduleDivisions
{
    [Key]
    public int DivisionsId { get; set; }

    [StringLength(50)]
    public string DivisionDesc { get; set; }
}