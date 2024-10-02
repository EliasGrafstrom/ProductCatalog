using Resources.Enums;
using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

public class ProductMenu
{
    private readonly Menu _productService = new Menu();

    public void CreateMenu()
    {
        Product product = new Product();

        Console.Clear();
        Console.WriteLine("Create New product");

        Console.Write("Enter product Name: ");
        product.Name = Console.ReadLine() ?? "";

        Console.Write("Enter product price: ");
        string priceInput = Console.ReadLine() ?? "";

        if (decimal.TryParse(priceInput, out decimal price))
        {
            product.Price = price;
        }
        else
        {
            Console.WriteLine("Invalid price input. Please enter a valid number.");
        }

        var result = _productService.AddToList(product);

        switch(result)
        {
            case ResultStatus.Success:
                Console.WriteLine("\nproduct was created successfully.");
                break;

            case ResultStatus.Exists:
                Console.WriteLine("\nproduct with the same product already exists.");
                break;

            case ResultStatus.Failed:
                Console.WriteLine("\nSomething went wrong while creating the product.");
                break;

            case ResultStatus.SuccessWithErrors:
                Console.WriteLine("\nproduct was created successfully. But product list was not saved.");
                break;
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }


    public void ViewAllMenu()
    {
        var productList = _productService.GetAllProducts();

        Console.Clear();
        Console.WriteLine("View All products\n");

        if (productList.Any())
        {
            foreach (Product product in productList)
            {
                Console.WriteLine($"{product.Name} <{product.Price}>");
            }
        }
        else
        {
            Console.WriteLine("No products in list\n");
        }


        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }


    public void ViewSingleMenu()
    {
        Console.Clear();
        Console.WriteLine("View Single product\n");

        Console.Write("Enter product product: ");
        var product = Console.ReadLine() ?? "";

        var Product = _productService.GetProduct(product);
        
        if (product != null)
        {
            Console.Clear();
            Console.WriteLine($"View Details for {product.Name}\n");

            Console.WriteLine("Name:".PadRight(10) + $"{product.Name}");
            Console.WriteLine("product:".PadRight(10) + $"{product.Price}");

        }
        else
        {
            Console.WriteLine($"No product was found.\n");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    internal void DeleteMenu()
    {
        Console.Clear();
        Console.WriteLine("Delete product\n");

        Console.Write("Enter product product: ");
        var product = Console.ReadLine() ?? "";

        var result = _productService.DeleteProduct(product);

        switch (result)
        {
            case ResultStatus.Success:
                Console.WriteLine("product was deleted successfully.");
                break;

            case ResultStatus.NotFound:
                Console.WriteLine("product was not found.");
                break;

            case ResultStatus.Failed:
                Console.WriteLine("\nSomething went wrong while deleting the product.");
                break;

            case ResultStatus.SuccessWithErrors:
                Console.WriteLine("\nproduct was deleted successfully. But product list was not saved.");
                break;
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}
