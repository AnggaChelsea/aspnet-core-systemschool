﻿using System.ComponentModel.DataAnnotations;

namespace NetAngularAuthWebApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

    }
}
