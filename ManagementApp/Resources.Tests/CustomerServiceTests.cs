using Resources.Enums;
using Resources.Models;
using Resources.Services;

namespace Resources.Tests;

public class ProductServiceTests
{
    [Fact]
    public void AddToList_ShouldReturnSuccess_WhenProductAddedToList()
    {
        // arrange
        Product product = new Product { Name = "abc", Price = Convert.ToDecimal(123.123)};
        ProductService productService = new ProductService();

        // act
        ResultStatus result = productService.AddToList(product);
        var productList = productService.GetAllProducts();

        // assert
        Assert.Equal(ResultStatus.Success, result);
        Assert.Single(productList);
        Assert.Equal("abc", productList.First().Name);
        Assert.Equal(Convert.ToDecimal(123.123), productList.First().Price);

        //cleanup
        File.Delete(Path.Combine(AppContext.BaseDirectory, "file.json"));
    }
}
