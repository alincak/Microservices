namespace FreeCource.API.Basket.Dtos
{
  public class BasketItemDto
  {
    public int Quantity { get; set; } //şimdilik sabit 1 ama kalsın.
    public string CourceId { get; set; }
    public string CourceName { get; set; }
    public decimal Price { get; set; }
  }
}
