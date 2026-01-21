namespace backend.DTOs
{
    public record ServiceRequest(int apparatusid, DateOnly Date, string description, string problems, int UserId);
}
