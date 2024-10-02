using Newtonsoft.Json;
using Resources.Enums;
using Resources.Models;

namespace Resources.Services;

public class ProductService
{
    private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "file.json");
    private readonly FileService _fileService = new FileService(_filePath);
    private List<Product> _productList = new List<Product>();
    private decimal price;

    public ResultStatus AddToList(Product Product)
    {
        try
        {
            GetProductsFromFile();

            if (_productList.Any(c => c.Price == Product.Price))
                return ResultStatus.Exists;

            _productList.Add(Product);
            
            var json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);
            if (result)
                return ResultStatus.Success;

            return ResultStatus.SuccessWithErrors;
        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    public IEnumerable<Product> GetAllProducts()
    {
        GetProductsFromFile();
        return _productList;
    }

    public Product GetProduct(string email)
    {
        GetProductsFromFile();     
        try
        {
            var Product = _productList.FirstOrDefault(c => c.Price == price);
            return Product ?? null!;
        }
        catch 
        {
            return null!;
        }
    }

    public ResultStatus DeleteProduct(string email)
    {
        try
        {
            GetProductsFromFile();
            var Product = GetProduct(email);

            if (Product == null)
                return ResultStatus.NotFound;

            _productList.Remove(Product);

            var json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);
            if (result)
                return ResultStatus.Success;

            return ResultStatus.SuccessWithErrors;
        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    public void GetProductsFromFile()
    {
        try
        {
            var content = _fileService.GetFromFile();

            if (!string.IsNullOrEmpty(content))
                _productList = JsonConvert.DeserializeObject<List<Product>>(content)!;
        }
        catch { }
    }



}