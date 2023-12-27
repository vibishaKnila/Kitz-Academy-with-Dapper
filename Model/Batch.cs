namespace Academy.Model
{
    public class Batch
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string Skills { get; set; }

        public string TrainerName { get; set; }

        public string BatchType { get; set; }

        public string BatchDuration { get; set; }

        public DateTime BatchDate { get; set; }

        public string BatchStatus { get; set; }
    }
}
