using System.ComponentModel.DataAnnotations;

namespace TestUngDung.Areas.Admin.Models
{
    public class ChangePasswordModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [Display(Name = "Mật khẩu cũ")]
        public string oldPassword { get; }

        [Required(ErrorMessage = "Mật khẩu mới phải khác mật khẩu cũ")]
        [Display(Name = "Mật khẩu mới")]
        public string newPassword { get; set; }
        public string reNewPassword { get; set; }
        public bool Status { get; set; }
    }
}