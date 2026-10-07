using System;
using System.Collections.Generic;
using System.Text;

namespace NobaCars.Core.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SocialSecurityNumber { get; set; }
    }
}
