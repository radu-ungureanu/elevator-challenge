namespace ElevatorChallenge.Domain
{
    public enum ElevatorStatus
    {
        Up,
        Down,
        Waiting
    }

    public static class ElevatorStatusExtensions
    {
        public static bool IsWaiting(this ElevatorStatus status)
        {
            return status == ElevatorStatus.Waiting;
        }

        public static bool IsGoingUp(this ElevatorStatus status)
        {
            return status == ElevatorStatus.Up;
        }

        public static bool IsGoingDown(this ElevatorStatus status)
        {
            return status == ElevatorStatus.Down;
        }
    }
}
