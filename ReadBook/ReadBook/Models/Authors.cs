using System;
using System.Collections.Generic;

namespace ReadBook.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Works = new HashSet<Works>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Infor { get; set; }
        public string Dob { get; set; }

        public virtual ICollection<Works> Works { get; set; }
    }
}
