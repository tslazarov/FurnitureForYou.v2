using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public enum ContactStatusType
    {
        [Display(Name = "Email")]
        NotProcessed = 1,
        [Display(Name = "Process")]
        Processing = 2,
        [Display(Name = "Processing")]
        Processed = 3
    }
}