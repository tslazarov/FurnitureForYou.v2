using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Models
{
    public enum OrderPaymentStatusType
    {
        [Display(Name = "Payed", ResourceType = typeof(Language))]
        Payed = 1,

        [Display(Name = "PaymentOnDelivery", ResourceType = typeof(Language))]
        PaymentOnDelivery = 2
    }
}