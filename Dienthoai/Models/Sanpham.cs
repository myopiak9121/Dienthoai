using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dienthoai.Models
{
    [Table("Sanpham")]
    public partial class Sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sanpham()
        {
            Chitietdonhang = new HashSet<Chitietdonhang>();
        }

        [Key]
        public int Masp { get; set; }
        [Required]
        [StringLength(50)]
        public string Tensp { get; set; }
        [Required]
        public decimal? Giatien { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Số lượng {1} tới {2}")]
        public int? Soluong { get; set; }

        [Column(TypeName = "ntext")]
        public string Mota { get; set; }

        public int? Thesim { get; set; }

        public int? Bonhotrong { get; set; }

        public bool? Sanphammoi { get; set; }

        public int? Ram { get; set; }

        [StringLength(10)]
        public string Anhbia { get; set; }

        public int? Mahang { get; set; }

        public int? Mahdh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietdonhang> Chitietdonhang { get; set; }

        public virtual Hangsanxuat Hangsanxuat { get; set; }

        public virtual Hedieuhanh Hedieuhanh { get; set; }
    }
}