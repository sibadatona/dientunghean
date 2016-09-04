namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuType")]
    public partial class MenuType
    {
        [Key]
        [StringLength(50)]
        public string TypeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
