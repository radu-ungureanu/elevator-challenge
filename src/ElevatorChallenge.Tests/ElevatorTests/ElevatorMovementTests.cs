using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.ElevatorTests
{
    public class ElevatorMovementTests
    {
        [Fact]
        public void WhenElevatorIsMovingBetweenFloors_ItArrivesCorrectly()
        {
            var initialFloor = 1;
            var destinationFloor = 8;

            var passedFloors = new List<int>();
            var reachedFloor = (int?)null;

            var sut = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            sut.ArrivedAtFloor += (s, e) => passedFloors.Add(e.Floor);
            sut.RequestedFloorReached += (s, e) => reachedFloor = e.Floor;

            sut.RequestFloor(destinationFloor);

            for (var i = 0; i < destinationFloor; i++)
            {
                sut.PerformOneStep();
            }

            var expectedFloors = new List<int> { 2, 3, 4, 5, 6, 7, 8 };

            Assert.Equal(expectedFloors, passedFloors);
            Assert.Equal(destinationFloor, reachedFloor);
            Assert.Equal(ElevatorStatus.Waiting, sut.Status);
        }
    }
}
