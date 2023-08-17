using ElevatorChallenge.Domain;
using ElevatorChallenge.Tests.Helpers;

namespace ElevatorChallenge.Tests.ElevatorTests
{
    public class ElevatorStatusTests
    {
        [Fact]
        public void WhenElevatorIsInitiated_ItIsInWaitingMode()
        {
            var sut = new Elevator(1, 1, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());

            Assert.Equal(ElevatorStatus.Waiting, sut.Status);
        }

        [Fact]
        public void WhenElevatorIsRequestedAtAHigherFloor_ItIsInGoingUpMode()
        {
            var sut = new Elevator(1, 1, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            sut.RequestFloor(4);
            sut.PerformOneStep();

            Assert.Equal(ElevatorStatus.Up, sut.Status);
        }

        [Fact]
        public void WhenElevatorIsRequestedAtTheSameFloor_ItDoesNotMove()
        {
            var floor = 1;

            var sut = new Elevator(1, floor, FloorCollectionMock.GenerateFloors(), new ElevatorTestMechanics());
            sut.RequestFloor(floor);
            sut.PerformOneStep();

            Assert.Equal(ElevatorStatus.Waiting, sut.Status);
            Assert.Equal(floor, sut.CurrentFloor);
            Assert.DoesNotContain(floor, sut.RequestedFloors);
        }
    }
}
