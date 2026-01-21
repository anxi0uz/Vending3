namespace backend.DTOs
{
    public record SalesRequest(int apparatusid, int productid, uint quantity, decimal totalprice, DateTime saledate, int PayType);
}
