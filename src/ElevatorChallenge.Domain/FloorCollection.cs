namespace ElevatorChallenge.Domain
{
    public class FloorCollection
    {
        private readonly List<Floor> _floors;

        public FloorCollection(IEnumerable<Floor> floors)
        {
            _floors = floors.ToList();
        }

        public Floor FloorAt(int floorNumber)
        {
            return _floors.First(floor => floor.FloorNumber == floorNumber);
        }

        public bool FloorExists (int floorNumber)
        {
            return _floors.Any(floor => floor.FloorNumber == floorNumber);
        }
    }
}
