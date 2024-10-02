using Resources.Enums;
using Resources.Models;

namespace MainApp.Menus;

public class Menu
{
    private readonly ProductMenu _productMenu = new ProductMenu();

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Main Menu");
        Console.WriteLine("1. Create Customer");
        Console.WriteLine("2. View All Customers");
        Console.WriteLine("3. View Single Customer");
        Console.WriteLine("4. Delete Customer");
        Console.Write("\nEnter your choice: ");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                _productMenu.CreateMenu();
                break;
            
            case "2":
                _productMenu.ViewAllMenu();
                break;
            
            case "3":
                _productMenu.ViewSingleMenu();
                break;
            
            case "4":
                _productMenu.DeleteMenu();
                break;
            
            default:
                Console.WriteLine("\nIncorrect choice, please try again by pressing any key.");
                Console.ReadKey();
                break;
        }
    }

    internal ResultStatus AddToList(Product product)
    {
        throw new NotImplementedException();
    }

    internal IEnumerable<Product> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    internal object GetProduct(string product)
    {
        throw new NotImplementedException();
    }
}
