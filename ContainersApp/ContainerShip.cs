using ConsoleApp2.containers;

namespace ConsoleApp2;

public class ContainerShip(int id, float _maxSpeed, int maxContainers, int maxWeight)
{
    private List<Container> _containers = [];

    public void LoadContainer(Container container)
    {
        if (_containers.Count < maxContainers && container.FreightMass + _containers.Sum(c => c.FreightMass) <= maxWeight)
            _containers.Add(container);
    }
    
    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }
    
    public void UnloadContainer(Container container)
    {
        _containers.Remove(container);
    }

    public void PrintContainerShip()
    {
        Console.WriteLine($"Container ship: {id}, Containers : {_containers.Count} {string.Join(", ", _containers.Select(c => c.SerialNumber))}  Products: {string.Join(", ", _containers.Select(c => c.Product.Name))}");
    }
    
    public void PrintContainers()
    {
        foreach (var container in _containers)
        {
            container.PrintContainer();
        }
    }

    public override string ToString()
    {
        return id.ToString();
    }

    public void TransferContainer(ContainerShip ship2, Container milkContainer)
    {
        UnloadContainer(milkContainer);
        ship2.LoadContainer(milkContainer);
    }

    public void ReplaceContainer(Container refrigeratedContainer, Container newLiquidContainer)
    {
        UnloadContainer(refrigeratedContainer);
        LoadContainer(newLiquidContainer);
    }
}