using ConsoleApp2.containers;
using ConsoleApp2.enums;

namespace ConsoleApp2;

public interface ISerialNumberGenerator
{
    public string GenerateSerialNumber(ContainerType type);
}