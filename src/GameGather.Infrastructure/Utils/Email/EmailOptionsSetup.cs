﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GameGather.Infrastructure.Utils.Email;

public sealed class EmailOptionsSetup : IConfigureOptions<EmailOptions>
{
    private const string SectionName = "Email";
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}