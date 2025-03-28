using ConsoleApp2.containers;
using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.Services;

public class ContainerFactory : IContainerFactory
{
    private readonly SerialNumberGenerator _numberGenerator;

    public ContainerFactory(SerialNumberGenerator numberGenerator)
    {
        _numberGenerator = numberGenerator;
    }
    public Container CreateContainer(
        ContainerType type, 
        int freightMass, 
        int height, 
        int weight, 
        int depth, 
        Product product, 
        int maxFreightMass, 
        string serialNumber = null)
    {
        if (string.IsNullOrEmpty(serialNumber))
        {
            serialNumber = _numberGenerator.GenerateSerialNumber(type);
        }

        switch (type)
        {
            case ContainerType.Gas:
                return new GasContainer(freightMass, height, weight, depth, product, maxFreightMass, serialNumber);
            case ContainerType.Refrigerated:
                return new RefrigeratedContainer(freightMass, height, weight, depth, product, maxFreightMass, serialNumber);
            case ContainerType.Liquid:
                return new LiquidContainer(freightMass, height, weight, depth, product, maxFreightMass, serialNumber);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), "Nieznany typ kontenera");
        }
    }

    public LiquidContainer CreateLiquidContainer(
        int height, 
        int weight, 
        int depth, 
        Product product, 
        int maxFreightMass)
    {
        string serialNumber = _numberGenerator.GenerateSerialNumber(ContainerType.Liquid);
        
        var container = new LiquidContainer(0, height, weight, depth, product, maxFreightMass, serialNumber);
        
        return container;
    }

    public RefrigeratedContainer CreateRefrigeratedContainer(
        int height,
        int weight,
        int depth,
        Product product,
        int maxFreightMass)
    {
        string serialNumber = _numberGenerator.GenerateSerialNumber(ContainerType.Refrigerated);
        
        var container = new RefrigeratedContainer(0, height, weight, depth, product, maxFreightMass, serialNumber);
        
        return container;
    }

    public GasContainer CreateGasContainer(
        int height,
        int weight,
        int depth,
        Product product,
        int maxFreightMass)
    {
        string serialNumber = _numberGenerator.GenerateSerialNumber(ContainerType.Gas);
        
        var container = new GasContainer(0, height, weight, depth, product, maxFreightMass, serialNumber);
        
        return container;
    }
}