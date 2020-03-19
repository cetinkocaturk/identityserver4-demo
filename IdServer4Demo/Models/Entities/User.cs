using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdServer4Demo.Models.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("UserId")]
        public string UserId { get; set; }

        [Column("UserUid")]
        public string UserUid { get; set; }

        [Column("UserPassword")]
        public string UserPassword { get; set; }

        [Column("IsActive"), DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
