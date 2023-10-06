﻿using Microsoft.AspNetCore.Http;

namespace Business.DTOs;

public class MailRequestDto
{
    public string? ToEmail { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<IFormFile>? Attachments { get; set; }
}

