using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GalloTube.Models;

[Table("Video")]
public class Video
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O Nome é obrigatório")]
    [StringLength(100, ErrorMessage = "O Título deve possuir no máximo 100 caracteres")]
    public string Name { get; set; }

    [Display(Description = "Descrição")]
    [StringLength(8000, ErrorMessage = "A descrição deve possuir no máximo 5000 caracteres")]
    public string Descriptions { get; set; }

    public DateTime UploadDate { get; set; }

    [Display(Name = "Duração (em minutos)")]
    [Required(ErrorMessage = "A Duração é obrigatória")]
    public Int16 Duration { get; set; }

    [StringLength(200)]
    [Display(Name = "Thumbnail")]
    public string Thumbnail { get; set; }

    [NotMapped]
    [Display(Name = "Video File")]
    public string VideoFile { get {
        return TimeSpan.FromMinutes(Duration) .ToString(@"%h'h 'mm'min'");
    }}

    public ICollection<VideoTag> Tags { get; set; }
}
