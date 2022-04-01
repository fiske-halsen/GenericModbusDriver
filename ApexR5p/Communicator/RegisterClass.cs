namespace ParticleCommunicator.Communicator
{
    public class RegisterClass : IEquatable<RegisterClass>
    {
        public int RegisterNumber { get; set; }
        public int RegisterValue { get; set; }

        public bool Equals(RegisterClass? other)
        {
            return other is RegisterClass @class &&
                   RegisterNumber == @class.RegisterNumber &&
                   RegisterValue == @class.RegisterValue;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RegisterNumber, RegisterValue);
        }
    }
}
