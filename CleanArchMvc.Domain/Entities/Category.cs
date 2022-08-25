﻿using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : Entity
{
    public string? Name { get; private set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
        Name = name;
    }

    public ICollection<Product> Products { get; private set; }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name. Name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name. Too short, minimum 3 characteres");
        Name = name;
    }
}
