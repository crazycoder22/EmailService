using System;

namespace EmailService.Common.Model
{
    public class EmailTemplateDTO
    {
        public Guid Id { get; set; }
        public string Template { get; set; }
        public string AppName { get; set; }
    }
}
