using ElevatorChallenge.Domain.Exceptions;
using ElevatorChallenge.Domain.Extensions;

namespace ElevatorChallenge.Domain
{
    public class Elevator
    {
        private readonly List<Person> _peopleInside;
        private readonly HashSet<int> _requests;
        private readonly FloorCollection _floors;
        private readonly IElevatorMechanics _elevatorMechanics;

        public int Id { get; }

        public ElevatorStatus Status { get; private set; }

        public int CurrentFloor { get; private set; }

        public int NumberOfPeopleInside => _peopleInside.Count;

        public IReadOnlySet<int> RequestedFloors => _requests;

        public event EventHandler<RequestedFloorReachedEventArgs> RequestedFloorReached;
        public event EventHandler<ArrivedAtFloorEventArgs> ArrivedAtFloor;

        public Elevator(int id, int initialFloor, FloorCollection floors, IElevatorMechanics elevatorMechanics)
        {
            if (!floors.FloorExists(initialFloor))
                throw new RequestedFloorNotAvailableException();

            _peopleInside = new();
            _requests = new();
            _floors = floors;

            Id = id;
            CurrentFloor = initialFloor;
            Status = ElevatorStatus.Waiting;
            _elevatorMechanics = elevatorMechanics;

            RequestedFloorReached = default!;
            ArrivedAtFloor = default!;
        }

        public void RequestFloor(int requestedFloor)
        {
            if (!_floors.FloorExists(requestedFloor))
                throw new RequestedFloorNotAvailableException();

            _requests.Add(requestedFloor);
        }

        public void PerformOneStep()
        {
            if (!_requests.Any())
                return;

            var requestedFloor = _requests.OrderBy(request => Math.Abs(CurrentFloor - request)).First();
            if (requestedFloor == CurrentFloor)
            {
                _requests.Remove(requestedFloor);
                return;
            }

            Status = requestedFloor > CurrentFloor ? ElevatorStatus.Up : ElevatorStatus.Down;
            MoveToNextFloor();

            if (CurrentFloor == requestedFloor)
            {
                _requests.Remove(requestedFloor);
                RequestedFloorReached?.Invoke(this, new RequestedFloorReachedEventArgs(CurrentFloor));
                Status = ElevatorStatus.Waiting;
            }
        }

        private void MoveToNextFloor()
        {
            _elevatorMechanics.Engage();
            CurrentFloor += Status == ElevatorStatus.Up ? 1 : -1;
            ArrivedAtFloor?.Invoke(this, new ArrivedAtFloorEventArgs(CurrentFloor, NumberOfPeopleInside));
        }

        public void PickupPeople(IReadOnlyList<Person> people)
        {
            _peopleInside.AddRange(people);

            var peopleBoarding = people
                .Where(person => _floors.FloorExists(person.DestinationFloor))
                .Select(person => person.DestinationFloor)
                .ToList();
            _requests.AddRange(peopleBoarding);
        }

        public void DropPeopleAtCurrentFloor()
        {
            var peopleDroppingAtCurrentFloor = _peopleInside
                .Where(person => person.DestinationFloor == CurrentFloor)
                .ToList();
            _peopleInside.RemoveRange(peopleDroppingAtCurrentFloor);
        }
    }
}
