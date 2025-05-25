﻿namespace ErpSystem.Application.Customers.DTOs;

public class CustomerDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;
}
