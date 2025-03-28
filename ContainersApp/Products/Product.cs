using ConsoleApp2.enums;

namespace ConsoleApp2.Products;

public class Product(string name, ProductType type, bool? isHazard = false, float? temperature = null)
{
    public string Name { get; } = name;
    public ProductType Type { get; } = type;
    public float? Temperature { get; set; } = temperature;
    public bool? IsHazard { get; set; } = isHazard;
}