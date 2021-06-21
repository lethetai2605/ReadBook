using System;
using System.Collections.Generic;

namespace ReadBook.Models
{
    public partial class Books
    {
        public Books()
        {
            Works = new HashSet<Works>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Substance { get; set; }
        public int BookTypeId { get; set; }
        public int SpecialTagId { get; set; }

        public virtual BookTypes BookType { get; set; }
        public virtual SpecialTags SpecialTag { get; set; }
        public virtual ICollection<Works> Works { get; set; }
    }
}
