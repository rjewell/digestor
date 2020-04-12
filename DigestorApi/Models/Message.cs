using System;

public class Message
{
    public long Id { get; set; }
    public int ChatId { get; set; }
    public DateTime Sent { get; set; }
    public string Sender { get; set; }
    public string Content { get; set; }
}