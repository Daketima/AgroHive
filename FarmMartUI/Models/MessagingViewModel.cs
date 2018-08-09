using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmMartUI.Models
{
    public class MessagingViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Write a Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "No recipient is selected")]
        public int ToApplicationUserId { get; set; }

        [Required(ErrorMessage = "No recipient is selected")]
        public int FromApplicationUserId { get; set; }

        public string ToPerson { get; set; }

        public string FromPerson { get; set; }

        [Required(ErrorMessage = "Write a Message body cannot be empty")]
        public string MessageToPost { get; set; }

        public bool IsNew { get; set; }

        public System.DateTime DateCreated { get; set; }
    }
}