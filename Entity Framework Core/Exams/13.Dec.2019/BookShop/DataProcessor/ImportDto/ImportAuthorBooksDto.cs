﻿using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
    public class ImportAuthorBooksDto
    {
        [JsonProperty("Id")]
        public int? BooksId { get; set; }
    }
}
