using System;

namespace TodoApi.DTOs;

public class ResponseAPI
{
  public string Message { get; set; } = string.Empty;
  public int StatusCode { get; set; }
  public string ErrorMessage { get; set; } = string.Empty;
}