using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.EffortStratefyTests
{
    public class GoingDownAndIsAboveRequestedFloorEffortStrategyTests
    {
        [Fact]
        public void GoingDownAndIsAboveRequestedFloorEffortStrategy_WhenElevatorIsAboveRequestedFloor()
        {
            var initialFloor = 8;
            var destinationFloor = 1;
            var targetFloor = 6;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingDownAndIsAboveRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(initialFloor - targetFloor - 1, effort);
        }

        [Fact]
        public void GoingDownAndIsAboveRequestedFloorEffortStrategy_WhenElevatorIsBelowRequestedFloor()
        {
            var initialFloor = 6;
            var destinationFloor = 4;
            var targetFloor = 7;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingDownAndIsAboveRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(int.MaxValue, effort);
        }
    }
}
