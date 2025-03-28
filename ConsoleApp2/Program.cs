using ConsoleApp2;
using ConsoleApp2.containers;
using ConsoleApp2.enums;
using ConsoleApp2.Products;
using ConsoleApp2.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<ISerialNumberGenerator, SerialNumberGenerator>();
services.AddSingleton<IContainerFactory, ContainerFactory>();

var serviceProvider = services.BuildServiceProvider();

var containerFactory = serviceProvider.GetRequiredService<IContainerFactory>();
var containers = new List<Container>();
var containerShips = new List<ContainerShip>();
var numberGenerator = new SerialNumberGenerator(containers);
       
var ship1 = new ContainerShip(1, 30, 100, 10000);
var ship2 = new ContainerShip(2, 25, 50, 5000);

Console.WriteLine("=== Demonstracja systemu zarządzania kontenerami ===\n");

try
{
    Console.WriteLine("1. Tworzenie kontenerów różnego typu:");
    var refrigeratedProduct = new Product("Banany", ProductType.Refrigerated, false, 13.5f);
    var refrigeratedContainer = containerFactory.CreateRefrigeratedContainer(
        250, 
        2000, 
        600,
        refrigeratedProduct,
        5000);
    
    var liquidProduct = new Product("Niebezpieczna ciecz", ProductType.Liquid, true);
    var liquidContainer = containerFactory.CreateLiquidContainer(
        height: 250,
        weight: 1500,
        depth: 600,
        product: liquidProduct, 
        maxFreightMass: 7000
    );
    
    var gasProduct = new Product("Gaz", ProductType.Gas, true);
    var gasContainer = containerFactory.CreateGasContainer(
        height: 200,
        weight: 1000,
        depth: 500,
        product: gasProduct,
        maxFreightMass: 3000
    );
    
    var milkProduct = new Product("Mleko", ProductType.Liquid, false);
    var milkContainer = containerFactory.CreateContainer(
        ContainerType.Liquid,
        0,
        250, 
        1500, 
        600,
        milkProduct,
        8000,
        numberGenerator.GenerateSerialNumber(ContainerType.Liquid));
    
    // 2. Wyświetlanie informacji o kontenerach
    Console.WriteLine("\n2. Wyświetlanie informacji o kontenerach:");
    refrigeratedContainer.PrintContainer();
    liquidContainer.PrintContainer();
    gasContainer.PrintContainer();
    milkContainer.PrintContainer();
    
    // 3. Załadowanie ładunku do kontenerów
    Console.WriteLine("\n3. Załadowanie ładunku do kontenerów:");
    refrigeratedContainer.AddFreight(4000);
    Console.WriteLine($"Kontener chłodniczy załadowany: {refrigeratedContainer.FreightMass} kg");
    
    milkContainer.AddFreight(7000);
    Console.WriteLine($"Kontener na mleko załadowany: {milkContainer.FreightMass} kg");
    
    // Poprawny załadunek niebezpiecznego kontenera (do 50% pojemności)
    liquidContainer.AddFreight(3400);
    Console.WriteLine($"Kontener na niebezpieczne płyny załadowany: {liquidContainer.FreightMass} kg");
    
    gasContainer.AddFreight(2500);
    Console.WriteLine($"Kontener gazowy załadowany: {gasContainer.FreightMass} kg");
    
    // 4. Załadowanie pojedynczego kontenera na statek
    Console.WriteLine("\n4. Załadowanie pojedynczego kontenera na statek:");
    ship1.LoadContainer(refrigeratedContainer);
    Console.WriteLine($"Kontener {refrigeratedContainer.SerialNumber} załadowany na statek {ship1}");
    
    // 5. Załadowanie listy kontenerów na statek
    Console.WriteLine("\n5. Załadowanie listy kontenerów na statek:");
    var containersToLoad = new List<Container> { liquidContainer, gasContainer };
    ship1.LoadContainers(containersToLoad);
    Console.WriteLine($"Załadowano {containersToLoad.Count} kontenery na statek {ship1}");
    
    // 6. Wyświetlenie informacji o statku i jego ładunku
    Console.WriteLine("\n6. Wyświetlenie informacji o statku i jego ładunku:");
    ship1.PrintContainerShip();
    
    // 7. Rozładowanie kontenera
    Console.WriteLine("\n7. Rozładowanie kontenera:");
    gasContainer.RemoveFreight(true);
    Console.WriteLine($"Kontener gazowy rozładowany, pozostało: {gasContainer.FreightMass} kg");
    
    // 8. Usunięcie kontenera ze statku
    Console.WriteLine("\n8. Usunięcie kontenera ze statku:");
    ship1.UnloadContainer(liquidContainer);
    Console.WriteLine($"Usunięto kontener {liquidContainer.SerialNumber} ze statku {ship1}");
    ship1.PrintContainerShip();
    
    // 9. Przeniesienie kontenera między statkami
    Console.WriteLine("\n9. Przeniesienie kontenera między statkami:");
    ship2.LoadContainer(milkContainer);
    Console.WriteLine($"Załadowano kontener {milkContainer.SerialNumber} na statek {ship2}");
    
    ship1.TransferContainer(ship2, milkContainer);
    Console.WriteLine($"Przeniesiono kontener {milkContainer.SerialNumber} z statku {ship2} na statek {ship1}");
    ship1.PrintContainerShip();
    ship2.PrintContainerShip();
    
    // 10. Zastąpienie kontenera na statku innym kontenerem
    Console.WriteLine("\n10. Zastąpienie kontenera na statku innym kontenerem:");
    var newLiquidProduct = new Product("Woda", ProductType.Liquid, false);
    var newLiquidContainer = containerFactory.CreateContainer(
        ContainerType.Liquid,
        0,
        250, 
        1500, 
        600,
        newLiquidProduct,
        7000,
        numberGenerator.GenerateSerialNumber(ContainerType.Liquid));
    
    newLiquidContainer.AddFreight(6000);
    
    ship1.ReplaceContainer(refrigeratedContainer, newLiquidContainer);
    Console.WriteLine($"Zastąpiono kontener {refrigeratedContainer.SerialNumber} kontenerem {newLiquidContainer.SerialNumber}");
    ship1.PrintContainerShip();
    
    // 11. Próba przepełnienia kontenera (obsługa wyjątku)
    Console.WriteLine("\n11. Próba przepełnienia kontenera (obsługa wyjątku):");
    try
    {
        // Ta operacja powinna zgłosić błąd, ponieważ niebezpieczny kontener
        // może być wypełniony tylko do 50% pojemności (3500kg)
        liquidContainer.AddFreight(5000);
    }
    catch (Exception ex) // Change to your specific OverfillException if it exists
    {
        Console.WriteLine($"Złapano wyjątek: {ex.Message}");
    }
    
    // 12. Próba załadowania zbyt wielu kontenerów na statek
    Console.WriteLine("\n12. Próba załadowania zbyt wielu kontenerów na statek:");
    try
    {
        // Tworzymy wiele kontenerów, żeby przekroczyć limit statku
        var manyContainers = new List<Container>();
        for (int i = 0; i < 55; i++)
        {
            var fruitProduct = new Product("Owoce", ProductType.Refrigerated, false, 10.0f);
            var container = containerFactory.CreateContainer(
                ContainerType.Refrigerated,
                0,
                250, 
                100, 
                600,
                fruitProduct,
                5000,
                numberGenerator.GenerateSerialNumber(ContainerType.Refrigerated));
            manyContainers.Add(container);
        }
        
        ship2.LoadContainers(manyContainers);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Złapano wyjątek: {ex.Message}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Wystąpił nieoczekiwany błąd: {ex.Message}");
}

Console.WriteLine("\n=== Demonstracja zakończona ===");
Console.ReadKey();
        

/*ContainerShip containerShip = new(1, 40, 10, 100000);

containerShips.Add(containerShip);


Random random = new();

for (int i = 0; i < 10; i++)
{
    var containerType = (ContainerType)random.Next(0, 3);
    Container container = containerFactory.CreateContainer(
        containerType,
        random.Next(100, 2000),
        400,
        random.Next(200, 300),
        random.Next(500, 1000),
        new ("Bananas", ProductType.Refrigerated, false, 13.3f),
        random.Next(20000, 40000),
        numberGenerator.GenerateSerialNumber(containerType)
    );
    
    containers.Add(container);
    
    containerShip.LoadContainer(container);
}*/
Console.Write("DONE");
