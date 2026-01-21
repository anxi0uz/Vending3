namespace backend.DTOs
{
    public record ServiceResponse(int id, int apparatusId, DateOnly date, string description, string problems, int userid);
}
