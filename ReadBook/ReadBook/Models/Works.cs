using System;
using System.Collections.Generic;

namespace ReadBook.Models
{
    public partial class Works
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int Id { get; set; }

        public virtual Authors Author { get; set; }
        public virtual Books Book { get; set; }
    }
}
