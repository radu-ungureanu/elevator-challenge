namespace ElevatorChallenge.Domain
{
    public class ElevatorHardwareMechanics : IElevatorMechanics
    {
        public void Engage()
        {
            Thread.Sleep(100);
        }
    }
}
