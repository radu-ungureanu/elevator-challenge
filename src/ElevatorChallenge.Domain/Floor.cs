namespace ElevatorChallenge.Domain
{
    public class Floor
    {
        private readonly List<Person> _waitingQueue;
        public IReadOnlyList<Person> WaitingQueue => _waitingQueue;

        public int FloorNumber { get; }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
            _waitingQueue = new();
        }

        public void ClearWaitingQueue() => _waitingQueue.Clear();

        public void AddPeopleToWaitingQueue(IEnumerable<Person> people) => _waitingQueue.AddRange(people);
    }
}
