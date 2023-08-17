using ElevatorChallenge.Domain.Exceptions;

namespace ElevatorChallenge.Domain
{
    public class Building
    {
        private readonly Queue<int> _requests;
        private readonly SelectionEffortStrategies _strategySelection;

        public FloorCollection Floors { get; }

        public IReadOnlyList<Elevator> Elevators { get; }

        public Building(IEnumerable<int> floorNumbers, int numberOfElevators, IElevatorMechanics elevatorMechanics)
        {
            _requests = new();
            _strategySelection = new();

            Floors = GenerateFloors(floorNumbers);
            Elevators = GenerateElevators(numberOfElevators, elevatorMechanics);
        }

        public void RequestElevatorAtFloor(int requestedFloor)
        {
            if (!Floors.FloorExists(requestedFloor))
                throw new RequestedFloorNotAvailableException();

            if (_requests.Contains(requestedFloor))
                return;

            _requests.Enqueue(requestedFloor);
        }

        public void Run()
        {
            while (true)
            {
                ProcessRequestsQueue();

                foreach (var elevator in Elevators)
                {
                    elevator.PerformOneStep();
                }
            }
        }

        private void ProcessRequestsQueue()
        {
            while (_requests.Any())
            {
                var floorNumberRequest = _requests.Dequeue();
                var elevator = FindBestElevator(floorNumberRequest);
                elevator.RequestFloor(floorNumberRequest);
            }
        }

        private Elevator FindBestElevator(int floor)
        {
            return Elevators
                .Select(elevator => new
                {
                    Elevator = elevator,
                    Effort = _strategySelection.BestEffort(elevator, floor)
                })
                .OrderBy(e => e.Effort)
                .First()
                .Elevator;
        }

        private static FloorCollection GenerateFloors(IEnumerable<int> floorNumbers)
        {
            var floors = floorNumbers.Select(floorNumber => new Floor(floorNumber)).ToList();
            return new FloorCollection(floors);
        }

        private List<Elevator> GenerateElevators(int numberOfElevators, IElevatorMechanics elevatorMechanics) =>
            Enumerable.Range(1, numberOfElevators)
                .Select(id =>
                {
                    var elevator = new Elevator(id, Constants.GroundFloor, Floors, elevatorMechanics);
                    elevator.RequestedFloorReached += OnElevatorArrived;

                    return elevator;
                })
                .ToList();

        private void OnElevatorArrived(object? sender, RequestedFloorReachedEventArgs args)
        {
            if (sender is not Elevator elevator)
                return;

            elevator.DropPeopleAtCurrentFloor();

            var floor = Floors.FloorAt(args.Floor);

            elevator.PickupPeople(floor.WaitingQueue);
            floor.ClearWaitingQueue();
        }
    }
}
