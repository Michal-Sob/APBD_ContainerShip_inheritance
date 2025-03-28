using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.containers;

public class RefrigeratedContainer : Container
{
    public override ContainerType Type => ContainerType.Refrigerated;
    public override char ShortName => 'R';
    private float _temperature;
    
    public RefrigeratedContainer(int freightMass, int height, int weight, int depth, Product product, int maxFreightMass, string serialNumber) 
        : base(freightMass, height, weight, depth, product, maxFreightMass, serialNumber)
    {
        if (product.Temperature.HasValue)
            _temperature = product.Temperature.Value;
    }
}