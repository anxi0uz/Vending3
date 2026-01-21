namespace backend.DTOs
{
    public record ProductResponse(int id, string name, string description, decimal price, uint quantity, uint minimalstock, decimal avgdailysales);
}
