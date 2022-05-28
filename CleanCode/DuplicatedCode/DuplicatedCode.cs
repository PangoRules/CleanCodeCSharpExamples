
using System;

namespace CleanCode.DuplicatedCode
{
    public class Time
    {
        public int Hours { get; }
        public int Minutes { get; }

        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public static Time GetTime(string admissionDateTime)
        {
            int time;
            int hours;
            int minutes;
            if (!string.IsNullOrWhiteSpace(admissionDateTime))
            {
                if (int.TryParse(admissionDateTime.Replace(":", ""), out time))
                {
                    hours = time / 100;
                    minutes = time % 100;
                }
                else
                {
                    throw new ArgumentException("admissionDateTime");
                }
            }
            else
                throw new ArgumentNullException("admissionDateTime");

            return new Time(hours, minutes);
        }
    }

    class DuplicatedCode
    {
        public void AdmitGuest(string name, string admissionDateTime)
        {
            // Some logic 
            // ...

            var createdTime = Time.GetTime(admissionDateTime);

            // Some more logic 
            // ...
            if (createdTime.Hours < 10)
            {

            }
        }

        public void UpdateAdmission(int admissionId, string name, string admissionDateTime)
        {
            // Some logic 
            // ...

            var createdTime = Time.GetTime(admissionDateTime);

            // Some more logic 
            // ...
            if (createdTime.Hours < 10)
            {

            }
        }
    }
}
