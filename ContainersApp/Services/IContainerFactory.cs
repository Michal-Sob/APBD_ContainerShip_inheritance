using ConsoleApp2.containers;
using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.Services;

public interface IContainerFactory
{
    public Container CreateContainer(ContainerType type, int freightMass, int height, 
        int weight, int depth, Product product, int maxFreightMass, string serialNumber);
    public LiquidContainer CreateLiquidContainer(int height, int weight, int depth, Product product, int maxFreightMass);
    public RefrigeratedContainer CreateRefrigeratedContainer(int height, int weight, int depth, Product product, int maxFreightMass);
    public GasContainer CreateGasContainer(int height, int weight, int depth, Product product, int maxFreightMass);
    
}