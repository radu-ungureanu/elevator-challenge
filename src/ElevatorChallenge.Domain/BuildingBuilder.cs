namespace ElevatorChallenge.Domain
{
    public class BuildingBuilder : IFloorBuilder, IElevatorBuilder, IBuildingBuilder
    {
        private readonly BuildingBuilderProps _props;

        public BuildingBuilder()
        {
            _props = new();
        }

        public IElevatorBuilder WithFloorsNumbers(IEnumerable<int> floorNumbers)
        {
            _props.FloorNumbers = floorNumbers.ToList();

            return this;
        }

        public IBuildingBuilder WithNumberOfElevators(int numberOfElevators)
        {
            _props.NumberOfElevators = numberOfElevators;
            return this;
        }

        public Building Build() => new(_props.FloorNumbers, _props.NumberOfElevators, new ElevatorHardwareMechanics());


        class BuildingBuilderProps
        {
            internal List<int> FloorNumbers { get; set; } = new();
            internal int NumberOfElevators { get; set; }
        }
    }

    public interface IFloorBuilder
    {
        IElevatorBuilder WithFloorsNumbers(IEnumerable<int> floorNumbers);
    }

    public interface IElevatorBuilder
    {
        IBuildingBuilder WithNumberOfElevators(int numberOfElevators);
    }

    public interface IBuildingBuilder
    {
        Building Build();
    }
}
