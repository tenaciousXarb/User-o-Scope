﻿using System.ComponentModel.DataAnnotations;

namespace AppUser.BusinessServices.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
