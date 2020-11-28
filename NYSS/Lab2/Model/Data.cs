using System;

namespace Lab2.Model
{
    [Serializable]
    public class Data
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Source { get; set; }
        public String Target { get; set; }
        public String ConfidentialityOffense { get; set; }
        public String ContinuityOffense { get; set; }
        public String AvailabilityOffense { get; set; }
    }
}