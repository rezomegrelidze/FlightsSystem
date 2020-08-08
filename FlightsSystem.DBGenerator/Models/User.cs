﻿using System.Security.Cryptography;

namespace FlightsSystem.DBGenerator.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
    }
}