namespace backend.DTOs
{
    public record ProductRequest(string name, string description, decimal price, uint quantity, uint minimalstock, decimal avgdailysales);
}
