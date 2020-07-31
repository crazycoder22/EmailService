using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.DataAccess.DataModel
{
    [Table("EmailTemplate")]
    public class EmailTemplate
    {
        [Key]
        public Guid Id { get; set; }
        public string Template { get; set; }
        public string AppName { get; set; }
    }
}
