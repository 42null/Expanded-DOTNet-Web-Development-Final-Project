using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Northwind.Controllers
{
    public class APIController(DataContext db) : Controller
    {
        // this controller depends on the NorthwindRepository
        private readonly DataContext _dataContext = db;

        [HttpGet, Route("api/product")]
        // returns all products
        public IEnumerable<Product> Get() => _dataContext.Products.OrderBy(p => p.ProductName);

        [HttpGet, Route("api/product/{id}")]
        // returns specific product

        // .Include("Category")
        public Product Get(int id) => _dataContext.Products.FirstOrDefault(p => p.ProductId == id);

        [HttpGet, Route("api/productWithRating/{id}")]
        // returns specific product
        // public Product GetProductWithRating(int id) => _dataContext.Products.Include("Category").FirstOrDefault(p => p.ProductId == id);
        // TODO: Not yet set up
        public Product GetProductWithRating(int id) => _dataContext.Products.Include("Reviews").FirstOrDefault(p => p.ProductId == id);
        // public ActionResult Index() => View(_dataContext.Discounts.Include("Product").Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now).Take(3));


        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => _dataContext.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product")]
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => _dataContext.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => _dataContext.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category")]
        // returns all categories
        public IEnumerable<Category> GetCategory() => _dataContext.Categories.Include("Products").OrderBy(c => c.CategoryName);

        [HttpPost, Route("api/addtocart")]
        // adds a row to the cartitem table
        public CartItem Post([FromBody] CartItemJSON cartItem) => _dataContext.AddToCart(cartItem);


        // Get all reviews related to the product
        [HttpGet, Route("api/product/reviews/{ProductId}")]
        public IEnumerable<Review> GetReviewsByProduct(int ProductId) => _dataContext.Reviews.Where(r => r.ProductId == ProductId);
        //  _dataContext.Products.FirstOrDefault(p => p.ProductId == id);
    }
}
