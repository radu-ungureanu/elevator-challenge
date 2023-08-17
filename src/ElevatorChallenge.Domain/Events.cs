namespace ElevatorChallenge.Domain
{
    public record RequestedFloorReachedEventArgs(int Floor);
    public record ArrivedAtFloorEventArgs(int Floor, int NumberOfPeopleInside);
}
