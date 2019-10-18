namespace DLL
{
    using DLL.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {

        public int Id { get; set; }

        public int AuthorId { get; set; }
        public string Title { get; set; }

        public int? Pages { get; set; }

        public int? Price { get; set; }
        public string Images { get; set; }
        public int rating { get; set; }
        public string Ganre { get; set; }
        public virtual Users Authors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual IEnumerable<Message> message { get; set; }
        
    }
}
