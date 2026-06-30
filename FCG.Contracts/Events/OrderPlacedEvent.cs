namespace FGC.Contracts.Events
{
    public record OrderPlacedEvent
    (
        int UserId,
        int GameId,
        decimal Price
    );
}
