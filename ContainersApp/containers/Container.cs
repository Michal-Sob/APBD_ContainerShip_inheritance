using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.containers;

public class Container : IContainer
{ 
    public int FreightMass { get; set; }
    public int MaxFreightMass { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; set; }
    public Product Product { get; set; }
    public virtual char ShortName  => '_';
    public virtual ContainerType Type => ContainerType.Standard;

    public Container(int freightMass, int height, int weight, int depth, Product product, int maxFreightMass, string serialNumber)
    {
        FreightMass = freightMass;
        Height = height;
        Weight = weight;
        Depth = depth;
        Product = product;
        MaxFreightMass = maxFreightMass;
        SerialNumber = serialNumber;
    }
    
    public void PrintContainer()
    {
        Console.WriteLine(
            $"Container: {SerialNumber}, FreightMass: {FreightMass} Height: {Height} Weight: {Weight} Depth: {Depth} Product: {Product.Name}");
    }
    
    public virtual void AddFreight(int freight)
    {
        if (FreightMass + freight > MaxFreightMass)
            throw new OverfillException("Max freight mass exceeded");
        FreightMass += freight;
    }
    
    public virtual void RemoveFreight(bool allFreight, int? freight = null)
    {
        if (allFreight)
        {
            FreightMass = 0;
            return;
        }
        
        if (freight is > 0)
            FreightMass -= freight.Value;
    }
    
    
}

public class OverfillException : Exception
{
    public OverfillException(string freightMassExceedsContainerWeight)
    {
        Console.WriteLine(freightMassExceedsContainerWeight);
    }
}