﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model.DB
{
    public class ParserTask
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int WebShopId { get; set; }
        public WebShop WebShop { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime EndDate { get; set; }

        public string IteratorSettings { get; set; }

        public string GrabberSettings { get; set; }
    }
}