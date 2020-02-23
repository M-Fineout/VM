using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Models
{
    public class CampModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        [Range(1,100)]
        public int Length { get; set; } = 1;

        /// <summary>
        /// AutoMapper will automatically bind these properties if they are prefixed with the name of the object (location).
        /// </summary>
        public string LocationVenue { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }

        //OR you can define the output syntax in the Profile that automapper references, in this case CampProfile

        //public string Venue { get; set; }
        //public string Address1 { get; set; }
        //public string Address2 { get; set; }
        //public string Address3 { get; set; }
        //public string CityTown { get; set; }
        //public string StateProvince { get; set; }
        //public string PostalCode { get; set; }
        //public string Country { get; set; }

        public ICollection<TalkModel> Talks { get; set; }

    }
}
