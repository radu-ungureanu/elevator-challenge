namespace ElevatorChallenge.Domain.Exceptions
{
    public class RequestedFloorNotAvailableException : ApplicationException
    {
        public RequestedFloorNotAvailableException() : base("Requested floor is not part of the building") { }
    }
}
