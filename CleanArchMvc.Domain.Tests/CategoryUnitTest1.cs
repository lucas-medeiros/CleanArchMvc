using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            _ = new Category(1, "Category Name");
        };
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category With Invalid Id")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            _ = new Category(-1, "Category Name");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact(DisplayName = "Create Category With Short Name")]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            _ = new Category(1, "Ca");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Too short, minimum 3 characteres");
    }

    [Fact(DisplayName = "Create Category With Missing Name")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () =>
        {
            _ = new Category(1, "");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName = "Create Category With Null Name")]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () =>
        {
            _ = new Category(1, name: null);
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }
}