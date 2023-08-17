using ElevatorChallenge.Domain;

var building = new BuildingBuilder()
    .WithFloorsNumbers(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
    .WithNumberOfElevators(10)
    .Build();

foreach (var elevator in building.Elevators)
{
    elevator.ArrivedAtFloor += (sender, args) =>
    {
        if (sender is not Elevator elevator)
            return;

        Console.WriteLine($"Elevator {elevator.Id} reached {args.Floor} with {args.NumberOfPeopleInside} people inside");
    };
}

var people1 = new List<Person>
{
    new Person(3),
    new Person(5),
    new Person(7),
    new Person(2),
    new Person(9),
    new Person(9),
    new Person(9),
    new Person(10),
    new Person(10)
};
building.Floors.FloorAt(4).AddPeopleToWaitingQueue(people1);

var people2 = new List<Person>
{
    new Person(3),
    new Person(5),
    new Person(7),
    new Person(9),
    new Person(9),
    new Person(9),
    new Person(10)
};
building.Floors.FloorAt(2).AddPeopleToWaitingQueue(people2);

new Thread(new ThreadStart(building.Run)).Start();

new Thread(new ThreadStart(() => building.RequestElevatorAtFloor(4))).Start();
Thread.Sleep(300);
new Thread(new ThreadStart(() => building.RequestElevatorAtFloor(2))).Start();

Console.Read();
Console.WriteLine("Finished");
