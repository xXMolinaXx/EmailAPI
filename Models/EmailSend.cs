using System;

namespace EmailAPI.Models;

public class EmailSend
{
  public int Id { get; set; }
  public string ToEmail { get; set; }
  public string Subject { get; set; }
  public string Body { get; set; } = string.Empty;
  public DateTime SentAt { get; set; } = DateTime.UtcNow;
  public bool IsSent { get; set; } = false;
  public string ErrorMessage { get; set; } = string.Empty;
}
