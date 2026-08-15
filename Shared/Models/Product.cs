namespace FullStackApp.Shared.Models
{
    /// <summary>
    /// Shared Product DTO used by both client and server projects.
    /// Fields are intentionally simple: id, name, price, stock, category.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Category? Category { get; set; }
    }
}