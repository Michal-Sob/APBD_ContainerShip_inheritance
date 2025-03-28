using ConsoleApp2.enums;
using ConsoleApp2.Products;

namespace ConsoleApp2.containers;

public interface IContainer
{
    public int FreightMass { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; set; }
    public Product Product { get; set; }
    public char ShortName { get; }
    public ContainerType Type { get; }
    public void PrintContainer();
    public void AddFreight(int freight);
    public void RemoveFreight(bool allFreight, int? freight = null);
}