using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lab5.Models
{
    public enum Question
    {
        Earth, Computer
    }
    public class Prediction
    {
        public int PredictionId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Url]
        [DisplayName("URL")]
        public string Url { get; set; }

        [Display(Name = "Question")]
        public Question Question { get; set; }
    }
}
