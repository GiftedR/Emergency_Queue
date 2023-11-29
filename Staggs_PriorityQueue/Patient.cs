namespace Staggs_EmergencyQueue
{
    internal class Patient
    {
        private string _lastName = "";
        private string _firstName = "";
        private DateOnly _DOB = DateOnly.MinValue;
        public readonly int _priority = 5;
        public Patient(string lastName, string firstName, DateOnly DOB, int priority) { 

            DateOnly Today = DateOnly.FromDateTime(DateTime.Now);
            if ((Today.Year - DOB.Year < 21  || Today.Year - DOB.Year > 65) && (priority > 1))
            {
                priority--;
            }

            _lastName = lastName;
            _firstName = firstName;
            _DOB = DOB;
            _priority = priority;
        }

        public override string ToString()
        {
            return $"{_lastName},{_firstName},{_DOB},{_priority}";
        }
    }
}
