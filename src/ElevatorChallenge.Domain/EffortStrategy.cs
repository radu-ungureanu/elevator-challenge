namespace ElevatorChallenge.Domain
{
    public class SelectionEffortStrategies
    {
        private readonly List<IEffortStrategy> _strategies;

        public SelectionEffortStrategies()
        {
            _strategies = new()
            {
                new WaitingElevatorEffortStrategy(),
                new GoingUpAndIsBelowRequestedFloorEffortStrategy(),
                new GoingDownAndIsAboveRequestedFloorEffortStrategy(),
                new GoingUpAndIsAboveRequestedFloorEffortStrategy(),
                new GoingDownAndIsBelowRequestedFloorEffortStrategy()
            };
        }

        public int BestEffort(Elevator elevator, int requestedFloor)
        {
            return _strategies.Min(strategy => strategy.Effort(elevator, requestedFloor));
        }
    }

    public interface IEffortStrategy
    {
        int Effort(Elevator elevator, int requestedFloor);
    }

    public class WaitingElevatorEffortStrategy : IEffortStrategy
    {
        public int Effort(Elevator elevator, int requestedFloor)
        {
            if (elevator.Status.IsWaiting())
            {
                return Math.Abs(elevator.CurrentFloor - requestedFloor);
            }

            return int.MaxValue;
        }
    }

    public class GoingUpAndIsBelowRequestedFloorEffortStrategy : IEffortStrategy
    {
        public int Effort(Elevator elevator, int requestedFloor)
        {
            if (elevator.Status.IsGoingUp() && elevator.CurrentFloor < requestedFloor)
            {
                return requestedFloor - elevator.CurrentFloor;
            }

            return int.MaxValue;
        }
    }

    public class GoingDownAndIsAboveRequestedFloorEffortStrategy : IEffortStrategy
    {
        public int Effort(Elevator elevator, int requestedFloor)
        {
            if (elevator.Status.IsGoingDown() && elevator.CurrentFloor > requestedFloor)
            {
                return elevator.CurrentFloor - requestedFloor;
            }

            return int.MaxValue;
        }
    }

    public class GoingUpAndIsAboveRequestedFloorEffortStrategy : IEffortStrategy
    {
        public int Effort(Elevator elevator, int requestedFloor)
        {
            if (elevator.Status.IsGoingUp() && elevator.CurrentFloor > requestedFloor)
            {
                var topFloorRequested = elevator.RequestedFloors.Max();

                var differenceBetweenTopRequestedFloorAndCurrentFloor = topFloorRequested - elevator.CurrentFloor;
                var differenceBetweenTopRequestedFloorAndNextRequestedFloor = topFloorRequested - requestedFloor;

                return differenceBetweenTopRequestedFloorAndCurrentFloor + differenceBetweenTopRequestedFloorAndNextRequestedFloor;
            }

            return int.MaxValue;
        }
    }

    public class GoingDownAndIsBelowRequestedFloorEffortStrategy : IEffortStrategy
    {
        public int Effort(Elevator elevator, int requestedFloor)
        {
            if (elevator.Status.IsGoingDown() && elevator.CurrentFloor < requestedFloor)
            {
                var bottomFloorRequested = elevator.RequestedFloors.Min();

                var differenceBetweenCurrentFloorAndLowestRequestedFloor = elevator.CurrentFloor - bottomFloorRequested;
                var differenceBetweenLowestRequestedFloorAndNextRequestedFloor = requestedFloor - bottomFloorRequested;

                return differenceBetweenCurrentFloorAndLowestRequestedFloor + differenceBetweenLowestRequestedFloorAndNextRequestedFloor;
            }

            return int.MaxValue;
        }
    }
}
