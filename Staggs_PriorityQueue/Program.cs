namespace Staggs_EmergencyQueue
{
    //Colton Staggs
    //IT 113
    //Notes:
    internal class Program
    {
        static PriorityQueue<Patient, int> ERQueue = new PriorityQueue<Patient, int>();
        static void Main(string[] args)
        {
            string projectOrigin = Environment.CurrentDirectory;
            string csvFile = projectOrigin + "\\Patients-1.csv";
            StreamReader sr = new StreamReader(csvFile);
            bool running = true;

            string? curLine = sr.ReadLine(); //Removes the Header Line and/or checks for no data
            string[] lineData = new string[4];

            while (curLine != null)
            {
                curLine = sr.ReadLine();
                if (curLine == null) continue;
                addToQueue(curLine, "csv");
            }

            sr.Close();
            
            while (running)
            {
                Console.WriteLine("(A)dd Patient\n(P)rocess Current Patient\n(L)ist All in Queue\n(Q)uit");

                char inKey = getInput();
                Console.WriteLine();
                switch (inKey)
                {
                    case 'A' or 'a':
                        Console.Clear();
                        Console.WriteLine($"Current Queue Count: {addToQueue("", "cmd")}");
                        break;

                    case 'P' or 'p':
                        Console.Clear();
                        Console.WriteLine($"Dequeued Patient: {removeFromQueue()} \n");
                        break;

                    case 'L' or 'l':
                        Console.Clear();
                        Console.WriteLine(listQueue());
                        break;

                    case 'Q' or 'q':
                        running = !running;
                        break;

                    default:
                        consError($"Invalid Option: {inKey}");
                        break;
                }
            }
        }

        static char getInput()
        {
            char[] inHistory = { '\0', '\0' };
            bool validKey = false;

            while (!validKey)
            {
                Console.SetCursorPosition(0, Console.CursorTop);

                inHistory[0] = inHistory[1];
                inHistory[1] = Console.ReadKey().KeyChar;

                if (inHistory[1] == '\r')
                {
                    validKey = !validKey;
                }
            }

            return inHistory[0];
        }

        static int addToQueue(string newPatient, string queStep)
        {
            string[] patientDetails = newPatient.Split(',');
            string inputPatientDetails = "";

            if (queStep == "cmd")
            {
                while (patientDetails.Length != 4)
                {

                    if (patientDetails.Length == 3)
                    {
                        string[] newArr = { patientDetails[0].Split(' ')[0], patientDetails[0].Split(' ')[1], patientDetails[1], patientDetails[2] };
                        patientDetails = newArr;
                        break;
                    }
                    else if (newPatient == "" && inputPatientDetails == "") { }
                    else
                    {
                        Console.Clear();
                        consError($"Invalid Patient Details: {inputPatientDetails}");
                        inputPatientDetails = "";
                    }

                    if (inputPatientDetails == "")
                    {
                        Console.WriteLine("Enter New Patient With the Details: ");
                        inputPatientDetails = Console.ReadLine() ?? "";
                    }
                    patientDetails = inputPatientDetails.Split(',');

                }
            }

            Patient tmpPatient = new Patient(patientDetails[0], patientDetails[1], DateOnly.FromDateTime(Convert.ToDateTime(patientDetails[2])), int.Parse(patientDetails[3]));
            ERQueue.Enqueue(tmpPatient, tmpPatient._priority);
            
            return ERQueue.Count;
        }

        static string removeFromQueue()
        {
            Patient returnPatient;
            returnPatient = ERQueue.Dequeue();
            return returnPatient.ToString();
        }

        static string listQueue()
        {
            Console.Clear();
            PriorityQueue<Patient, int> QueueCopy = new PriorityQueue<Patient, int>();
            string returnString = "Name, DOB, Priority";

            int loopCount = ERQueue.Count;
            for (int i = 0; i < loopCount; i++)
            {
                Patient tmpQueue = ERQueue.Dequeue();
                returnString += "\n" + tmpQueue;
                QueueCopy.Enqueue(tmpQueue, tmpQueue._priority);
            }

            loopCount = QueueCopy.Count;
            for (int i = 0; i < loopCount; i++)
            {
                Patient tmpQueue = QueueCopy.Dequeue();
                ERQueue.Enqueue(tmpQueue, tmpQueue._priority);
            }

            returnString += $"\n================ { ERQueue.Count }, { loopCount }, {returnString.Length} \n";
            return returnString;
        }

        static void consError(string consString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(consString);
            Console.ResetColor();
        }
    }
}