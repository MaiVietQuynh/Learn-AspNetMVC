using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn_AspNetMVC.Models.Contacts
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Display(Name ="Ho Ten")]
        [Required(ErrorMessage ="Phai nhap {0}")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phai nhap {0}")]
        [StringLength(100)]
        [Display(Name = "Dia chi Email")]
        [EmailAddress(ErrorMessage ="Phai nhap dinh dang {0}")]
        public string Email { get; set; }
        public DateTime DateSent { get; set; }
        public string Message { get; set; }
        [StringLength(50)]
        [Phone(ErrorMessage ="Phai nhap {0}")]
        [Display(Name = "So dien thoai")]
        public string Phone { get; set; }
    }
}
