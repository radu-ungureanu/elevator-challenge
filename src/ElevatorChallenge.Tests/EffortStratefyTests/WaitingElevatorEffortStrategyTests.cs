using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.EffortStratefyTests
{
    public class WaitingElevatorEffortStrategyTests
    {
        [Fact]
        public void WaitingElevatorEffortStrategy_WhenElevatorIsBelowRequestedFloor()
        {
            var initialFloor = 1;
            var targetFloor = 8;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());

            var sut = new WaitingElevatorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(targetFloor - initialFloor, effort);
        }

        [Fact]
        public void WaitingElevatorEffortStrategy_WhenElevatorIsAboveRequestedFloor()
        {
            var initialFloor = 8;
            var targetFloor = 1;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());

            var sut = new WaitingElevatorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(initialFloor - targetFloor, effort);
        }

        [Fact]
        public void WaitingElevatorEffortStrategy_WhenElevatorIsOnSameFloorAsRequestedFloor()
        {
            var initialFloor = 8;
            var targetFloor = 8;

            var elevator = new Elevator(1, initialFloor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());

            var sut = new WaitingElevatorEffortStrategy();
            var effort = sut.Effort(elevator, targetFloor);

            Assert.Equal(initialFloor - targetFloor, effort);
        }
    }
}
