using FFY.Resources;
using System.ComponentModel.DataAnnotations;

namespace FFY.Web.Areas.Administration.Models.ProductManagement
{
    public class RoomPartialViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Language), ErrorMessageResourceName = "RoomNameRequired")]
        [Display(Name = "RoomName", ResourceType = typeof(Language))]
        public string Name { get; set; }

        [Display(Name = "RoomImage", ResourceType = typeof(Language))]
        public string ImagePath { get; set; }
    }
}