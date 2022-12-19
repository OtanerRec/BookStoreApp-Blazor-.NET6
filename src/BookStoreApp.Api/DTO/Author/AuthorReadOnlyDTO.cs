﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.DTO.Author
{
    public class AuthorReadOnlyDTO : BaseDTO
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Bio { get; set; }
    }
}
