namespace FreeCource.API.Basket.Dtos
{
  public class BasketItemDto
  {
    public int Quantity { get; set; } //şimdilik sabit 1 ama kalsın.
    public string CourseId { get; set; }
    public string CourseName { get; set; }
    public decimal Price { get; set; }
  }
}
