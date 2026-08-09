using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Authentication;

[Table("AspNetRoles", Schema = "public")]
public class Role
{
    [Key]
    [Column("Id")]
    public string Id { get; set; } = string.Empty;

    [Column("Name")]
    [MaxLength(256)]
    public string? Name { get; set; }

    [Column("NormalizedName")]
    [MaxLength(256)]
    public string? NormalizedName { get; set; }

    [Column("ConcurrencyStamp")]
    public string? ConcurrencyStamp { get; set; }
}