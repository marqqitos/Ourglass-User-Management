using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementCommons.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        [JsonRequired]
        public string Username { get; set; }
        
        [JsonRequired]
        public string FirstName { get; set; }
        
        [JsonRequired]
        public string LastName { get; set; }
    }
}
