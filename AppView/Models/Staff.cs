using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public partial class Staff
    {
        public Staff()
        {
            DepartmentFacilities = new HashSet<DepartmentFacility>();
            StaffMajorFacilities = new HashSet<StaffMajorFacility>();
        }

        [Required(ErrorMessage = "Tên nhân viên không được bỏ trống.")]
        [StringLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email FE không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email FE không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email FE không được vượt quá 100 ký tự.")]
        [RegularExpression(@"^[^@\s]+@fe\.edu\.vn$", ErrorMessage = "Email FE phải kết thúc với @fe.edu.vn và không chứa khoảng trắng hoặc tiếng Việt.")]
        public string? AccountFe { get; set; }

        [Required(ErrorMessage = "Email FPT không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email FPT không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email FPT không được vượt quá 100 ký tự.")]
        [RegularExpression(@"^[^@\s]+@fpt\.edu\.vn$", ErrorMessage = "Email FPT phải kết thúc với @fpt.edu.vn và không chứa khoảng trắng hoặc tiếng Việt.")]
        public string? AccountFpt { get; set; }

        [Required(ErrorMessage = "Mã nhân viên không được bỏ trống.")]
        [StringLength(15, ErrorMessage = "Mã nhân viên không được vượt quá 15 ký tự.")]
        public string? StaffCode { get; set; }

        public byte? Status { get; set; }

        public long? CreatedDate { get; set; }

        public long? LastModifiedDate { get; set; }

        public Guid Id { get; set; }

        public virtual ICollection<DepartmentFacility> DepartmentFacilities { get; set; }
        public virtual ICollection<StaffMajorFacility> StaffMajorFacilities { get; set; }
    }
}
