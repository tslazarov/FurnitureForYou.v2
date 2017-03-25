using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public enum OrderStatusType
    {
        [Display(Name = "Processing", ResourceType = typeof(Language))]
        Processing = 1,

        [Display(Name = "Sent", ResourceType = typeof(Language))]
        Sent = 2,

        [Display(Name = "Delivered", ResourceType = typeof(Language))]
        Delivered = 3,

        [Display(Name = "Rejected", ResourceType = typeof(Language))]
        Rejected = 4
    }
}