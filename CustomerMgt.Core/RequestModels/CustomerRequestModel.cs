using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMgt.Core.RequestModels
{
    public class CustomerRequestModel
    {
        [MinLength(2, ErrorMessage = "First name should be atleast 2 characters")]
        [MaxLength(100, ErrorMessage = "First name should not be more than 100 characters")]
        [Required(ErrorMessage = "The First Name is required")]
        public string FirstName { get; set; } = string.Empty;
        

        [MinLength(2, ErrorMessage = "Last name should be atleast 2 characters")]
        [MaxLength(100, ErrorMessage = "Last name should not be more than 100 characters")]
        [Required(ErrorMessage = "The Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [MinLength(8, ErrorMessage = "Invalid phone number")]
        [MaxLength(15, ErrorMessage = "Invalid Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
