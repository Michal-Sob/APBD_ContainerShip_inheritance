using ConsoleApp2.containers;
using ConsoleApp2.enums;

namespace ConsoleApp2;

public class SerialNumberGenerator : ISerialNumberGenerator
{
    private readonly List<Container> _containers;

    public SerialNumberGenerator(List<Container> containers)
    {
        _containers = containers;
    }
    
    public string GenerateSerialNumber(ContainerType type)
    {
        var lastContainerNumber = _containers.Count(c => c.Type == type);
        return $"KON-{GetContainerShortName(type)}-{lastContainerNumber + 1}";
    }
    
    private string GetContainerShortName(ContainerType type)
    {
        return type switch
        {
            ContainerType.Standard => "S",
            ContainerType.Liquid => "L",
            ContainerType.Refrigerated => "R",
            ContainerType.Gas => "G",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}