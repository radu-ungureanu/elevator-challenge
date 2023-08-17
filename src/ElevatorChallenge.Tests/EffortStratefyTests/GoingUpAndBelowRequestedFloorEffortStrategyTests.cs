using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.EffortStratefyTests
{
    public class GoingUpAndBelowRequestedFloorEffortStrategyTests
    {
        [Fact]
        public void GoingUpAndBelowRequestedFloorEffortStrategy_WhenElevatorIsBelowRequestedFloor()
        {
            var initialFloor = 1;
            var destinationFloor = 8;
            var targetFloor = 6;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingUpAndIsBelowRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(targetFloor - initialFloor - 1, effort);
        }

        [Fact]
        public void GoingUpAndBelowRequestedFloorEffortStrategy_WhenElevatorIsAboveRequestedFloor()
        {
            var initialFloor = 4;
            var destinationFloor = 6;
            var targetFloor = 3;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            elevator.RequestFloor(destinationFloor);
            elevator.PerformOneStep();

            var sut = new GoingUpAndIsBelowRequestedFloorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(int.MaxValue, effort);
        }
    }
}
