using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public override ContainerType Type => ContainerType.Liquid;
    public override char ShortName => 'L';
    public bool IsHazard { get; set; }

    public LiquidContainer(int freightMass, int height, int weight, int depth, Product product, int maxFreightMass, string serialNumber) 
        : base(freightMass, height, weight, depth, product, maxFreightMass, serialNumber)
    {
        
    }
    
    public void SendHazardNotification(string message)
    {
        Console.WriteLine($"Container: {ShortName}, {message}");
    }
    
    public override void AddFreight(int freight)
    {
        if (IsHazard)
            if (FreightMass + freight > MaxFreightMass / 2)
                SendHazardNotification("Attempt to add hazardous freight to a full container");
            else
                base.AddFreight(freight); 
        else if (FreightMass + freight > MaxFreightMass * 0.9)
            SendHazardNotification("Attempt to add freight to a full container");
        else
            base.AddFreight(freight);
    }
}