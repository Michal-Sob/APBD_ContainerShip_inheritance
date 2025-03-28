using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.containers;

public class GasContainer : Container, IHazardNotifier
{
    
    
    public override ContainerType Type => ContainerType.Gas;
    public override char ShortName => 'G';
    
    public GasContainer(int freightMass, int height, int weight, int depth, Product product, int maxFreightMass, string serialNumber) 
        : base(freightMass, height, weight, depth, product, maxFreightMass, serialNumber)
    {
    }
    
    public override void RemoveFreight(bool allFreight, int? freight = null)
    {
        if (allFreight)
        {
            FreightMass = Convert.ToInt32(FreightMass - FreightMass * 0.05);
            
            return;
        }
        
        if (freight is > 0)
            FreightMass -= freight.Value;
    }

    public void SendHazardNotification(string message)
    {
        Console.WriteLine($"Container: {ShortName}, {message}");
    }   
}