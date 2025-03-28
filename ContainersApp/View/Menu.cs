namespace ConsoleApp2.View;

public class Menu
{
    public Menu()
    {
        
    }
    public void ShowMenu()
    {
        Console.WriteLine("1. Add container ship");
        Console.WriteLine("2. Add container");
        Console.WriteLine("3. Remove container");
        Console.WriteLine("4. Remove container ship");
        Console.WriteLine("5. Add product to container");
        Console.WriteLine("6. Remove product from container");
        Console.WriteLine("7. List products");
        Console.WriteLine("8. List containers");
        /*Console.WriteLine("7. List products in container");
        Console.WriteLine("8. List products in container by type");
        Console.WriteLine("9. List products in container by hazard");
        Console.WriteLine("10. List products in container by temperature");
        Console.WriteLine("11. Send hazard notification");*/
        Console.WriteLine("12. Exit");
    }
    
    public void AddContainerShip()
    {
        Console.WriteLine("Enter max speed: ");
        var maxSpeed = float.Parse(Console.ReadLine());
        Console.WriteLine("Enter max containers: ");
        var maxContainers = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter max weight: ");
        var maxWeight = int.Parse(Console.ReadLine());
    }
}