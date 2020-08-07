using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace BookShop.DataProcessor.ImportDto
{
    [JsonObject("Author")]
    public class ImportAuthorDto
    {
        public ImportAuthorDto()
        {
            this.Books = new List<ImportAuthorBooksDto>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("Phone")]
        [RegularExpression(@"^(\d{3})\-(\d{3})\-(\d{4})$")]
        public string Phone { get; set; }

        public ICollection<ImportAuthorBooksDto> Books { get; set; }
    }
}
