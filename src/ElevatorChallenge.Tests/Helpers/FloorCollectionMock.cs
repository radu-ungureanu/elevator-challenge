using ElevatorChallenge.Domain;

namespace ElevatorChallenge.Tests.Helpers
{
    public class FloorCollectionMock : FloorCollection
    {
        public FloorCollectionMock(IEnumerable<Floor> floors) : base(floors)
        {
        }

        public static FloorCollection GenerateFloors()
        {
            var floorNumbers = Enumerable.Range(0, 10);
            var floors = floorNumbers.Select(number => new Floor(number));

            return new FloorCollection(floors);
        }
    }
}
