using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsFor.Models
{
    [ComplexType]
    public class FoodPic
    {
        
        public int Id { get; set; }

        [Display(Name ="Posted by:")]
        public string PostUserName { get; set; }
        
        
        [Display(Name = "Title")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        //TODO: Make this a conditional with ImgUpload
       // [Required(ErrorMessage = "Picture uploads are required")]
        public string ImgUrl { get; set; }


        //File to upload and store in wwwroot
        [Display(Name = "Image Upload")]
        public IFormFile ImgUpload { get; set; }


        [Display(Name = "Description")]
        [StringLength(400)]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Tag names cannot be empty")]
        //[Display(Name = "Tags")]
        //public List<FoodTag> Tags { get; set; }


        // [DataType(DataType.DateTime)]
        public DateTimeOffset DatePosted { get; set; }

    }
}
