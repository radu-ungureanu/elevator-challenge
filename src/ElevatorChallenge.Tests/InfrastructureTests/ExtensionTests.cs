using ElevatorChallenge.Domain.Extensions;

namespace ElevatorChallenge.Tests.InfrastructureTests
{
    public class ExtensionTests
    {
        [Fact]
        public void HashSet_AddRange_AddsCorrectItems()
        {
            var sut = new HashSet<int>();
            var newItems = new[] { 1, 2, 3 };

            sut.AddRange(newItems);

            Assert.Equal(newItems.Length, sut.Count);
        }

        [Fact]
        public void List_RemoveRange_RemovesCorrectItems()
        {
            var sut = new List<int> { 1, 2, 3, 4 };
            var itemsToRemove = new int[] { 1, 2 };

            sut.RemoveRange(itemsToRemove);

            Assert.Equal(2, sut.Count);
        }
    }
}
