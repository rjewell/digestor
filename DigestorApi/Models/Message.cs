using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Message
{
    public Message()
    {
        Sent = DateTime.Now;
    }

    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public int ChatId { get; set; }

    public DateTime Sent{ get; set; }
    public string Sender { get; set; }
    public string Content { get; set; }
}