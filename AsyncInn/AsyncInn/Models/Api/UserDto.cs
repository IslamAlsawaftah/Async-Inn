﻿using System.Collections.Generic;

namespace AsyncInn.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Token { get; set; }

        public IList<string> Roles { get; set; }
    }
}
