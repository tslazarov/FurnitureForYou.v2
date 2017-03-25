using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public enum ContactStatusType
    {
        [Display(Name = "NotProcessed", ResourceType = typeof(Language))]
        NotProcessed = 1,
        [Display(Name = "Processing", ResourceType = typeof(Language))]
        Processing = 2,
        [Display(Name = "Processed", ResourceType = typeof(Language))]
        Processed = 3
    }
}