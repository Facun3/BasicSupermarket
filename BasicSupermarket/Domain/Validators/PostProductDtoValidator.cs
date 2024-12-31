// using BasicSupermarket.Domain.Dto;
// using FluentValidation;
//
// namespace BasicSupermarket.Domain.Validators;
//
// public class PostProductDtoValidator: AbstractValidator<CreateProductRequestDto>
// {
//     public PostProductDtoValidator()
//     {
//         RuleFor(x => x.Name)
//             .NotEmpty().WithMessage("The product name is required.")
//             .MaximumLength(100).WithMessage("The product name must not exceed 100 characters.");
//
//         RuleFor(x => x.Description)
//             .MaximumLength(500).WithMessage("The description must not exceed 500 characters.");
//
//         RuleFor(x => x.ImageUrl)
//             .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
//             .WithMessage("The image URL must be valid.")
//             .When(x => !string.IsNullOrEmpty(x.ImageUrl));
//
//         RuleFor(x => x.Price)
//             .GreaterThan(0).WithMessage("The price must be greater than zero.");
//         
//         RuleFor(x => x.CategoryId)
//             .GreaterThan(0).WithMessage("The category ID must be greater than zero.");
//     }
// }