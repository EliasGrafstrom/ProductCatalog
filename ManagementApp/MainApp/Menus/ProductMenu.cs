using Resources.Enums;
using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

public class ProductMenu
{
    private readonly ProductService _productService = new ProductService(); // using ProductService

    public void CreateMenu()
    {
        Product product = new Product(); // UID will automatically be generated

        Console.Clear();
        Console.WriteLine("Create New Product");

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
            return; // Exit early if the price is invalid
        }

        var result = _productService.AddToList(product);

        switch (result)
        {
            case ResultStatus.Success:
                Console.WriteLine($"\nProduct was created successfully with ID: {product.Id}");
                break;

            case ResultStatus.Exists:
                Console.WriteLine("\nA product with the same name already exists.");
                break;

            case ResultStatus.Failed:
                Console.WriteLine("\nSomething went wrong while creating the product.");
                break;

            case ResultStatus.SuccessWithErrors:
                Console.WriteLine("\nProduct was created successfully, but the product list was not saved.");
                break;
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void ViewAllMenu()
    {
        var productList = _productService.GetAllProducts();

        Console.Clear();
        Console.WriteLine("View All Products\n");

        if (productList.Any())
        {
            foreach (Product product in productList)
            {
                Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Price: {product.Price:C}");
            }
        }
        else
        {
            Console.WriteLine("No products in the list.\n");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void ViewSingleMenu()
    {
        Console.Clear();
        Console.WriteLine("View Single Product\n");

        Console.Write("Enter product name: ");
        var productName = Console.ReadLine() ?? "";

        var product = _productService.GetProduct(productName);

        if (product != null)
        {
            Console.Clear();
            Console.WriteLine($"View Details for {product.Name}\n");

            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Price: {product.Price:C}");
        }
        else
        {
            Console.WriteLine("No product was found.\n");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void DeleteMenu()
    {
        Console.Clear();
        Console.WriteLine("Delete Product\n");

        Console.Write("Enter product name: ");
        var productName = Console.ReadLine() ?? "";

        var result = _productService.DeleteProduct(productName);

        switch (result)
        {
            case ResultStatus.Success:
                Console.WriteLine("Product was deleted successfully.");
                break;

            case ResultStatus.NotFound:
                Console.WriteLine("Product was not found.");
                break;

            case ResultStatus.Failed:
                Console.WriteLine("Something went wrong while deleting the product.");
                break;

            case ResultStatus.SuccessWithErrors:
                Console.WriteLine("Product was deleted successfully, but the product list was not saved.");
                break;
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}
