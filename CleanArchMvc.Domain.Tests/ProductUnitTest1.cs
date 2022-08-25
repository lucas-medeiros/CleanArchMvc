using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
        };
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            _ = new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
        };
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            _ = new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
        };
        action.Should().Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
             .WithMessage("Invalid name. Too short, minimum 3 characteres");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99,
                        "product image toooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooogggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name. Too long, maximum 250 characteres");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        };
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        };
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        };
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", -9.99m, 99, "");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () =>
        {
            _ = new Product(1, "Pro", "Product Description", 9.99m, value, "product image");
        };
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
}
