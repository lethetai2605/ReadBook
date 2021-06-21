using System;
using System.Collections.Generic;

namespace ReadBook.Models
{
    public partial class BookTypes
    {
        public BookTypes()
        {
            Books = new HashSet<Books>();
        }

        public int Id { get; set; }
        public string BookType { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
