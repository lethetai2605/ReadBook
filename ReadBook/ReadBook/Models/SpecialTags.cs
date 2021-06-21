using System;
using System.Collections.Generic;

namespace ReadBook.Models
{
    public partial class SpecialTags
    {
        public SpecialTags()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string SpecialTag { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
