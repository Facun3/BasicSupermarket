namespace BasicSupermarket.Domain.Dtos;

public class CreateProductRequestDto
{
    public String Name { get; set; }
    public String Description { get; set; }
    public String ImageUrl { get; set; }
}