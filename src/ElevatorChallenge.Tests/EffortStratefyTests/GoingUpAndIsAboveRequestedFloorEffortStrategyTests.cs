using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.EffortStratefyTests
{
    public class GoingUpAndIsAboveRequestedFloorEffortStrategyTests
    {
        [Fact]
        public void GoingUpAndIsAboveRequestedFloorEffortStrategy_WhenElevatorIsAboveRequestedFloor()
        {
            var initialFloor = 4;
            var destinationFloor = 8;
            var targetFloor = 2;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingUpAndIsAboveRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(9, effort);
        }

        [Fact]
        public void GoingUpAndIsAboveRequestedFloorEffortStrategy_WhenElevatorIsBelowRequestedFloor()
        {
            var initialFloor = 4;
            var destinationFloor = 8;
            var targetFloor = 7;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingUpAndIsAboveRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(int.MaxValue, effort);
        }
    }
}
