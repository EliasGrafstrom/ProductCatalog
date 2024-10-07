using Newtonsoft.Json;
using Resources.Enums;
using Resources.Models;

namespace Resources.Services;

public class ProductService
{
    private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "file.json");
    private readonly FileService _fileService = new FileService(_filePath);
    private List<Product> _productList = new List<Product>();

    public ResultStatus AddToList(Product product)
    {
        try
        {
            GetProductsFromFile();

            if (_productList.Any(c => c.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
                return ResultStatus.Exists;

            _productList.Add(product);

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

    public Product GetProduct(string name)
    {
        GetProductsFromFile();
        try
        {
            // Search by name
            var product = _productList.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return product ?? null!;
        }
        catch
        {
            return null!;
        }
    }

    public ResultStatus DeleteProduct(string name)
    {
        try
        {
            GetProductsFromFile();
            var product = GetProduct(name);

            if (product == null)
                return ResultStatus.NotFound;

            _productList.Remove(product);

            var json = JsonConvert.SerializeObject(_productList, Formatting.Indented);
            var result = _fileService.SaveToFile(json);
            if (result)
            {
                return ResultStatus.Success;
            }

            return ResultStatus.SuccessWithErrors;
        }
        catch
        {
            return ResultStatus.Failed;
        }
    }

    private void GetProductsFromFile()
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
