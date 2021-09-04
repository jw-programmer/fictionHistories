using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Src.Models
{
    public class Author: IdentityUser
    {
        [Required]
        [EmailAddress]
        public override string Email { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }

        public int Age
        {
            get { return CalcAge(DateTime.Now); }
        }

        [JsonIgnore]
        public virtual IList<History> Histories { get; set; }

        [JsonIgnore]
        public virtual IList<Comment> Comments { get; set; }

        public int CalcAge(DateTime today)
        {
            var yeas = today.Year - BirthDate.Year;
            if ((BirthDate.Month > today.Month) || (BirthDate.Month == today.Month && BirthDate.Day > today.Day))
            {
                yeas--;
            }

            return yeas;
        }
    }
}