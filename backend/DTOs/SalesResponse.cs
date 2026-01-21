namespace backend.DTOs
{
    public record SalesResponse(int id, int apparatusid, int productid, uint quantity, decimal totalprice, DateTime saledate, string paytype);
}
