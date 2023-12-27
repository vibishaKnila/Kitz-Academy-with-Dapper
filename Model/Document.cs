namespace Academy.Model
{
    public class Document
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string DocumentDetails { get; set; }

        public string DocumentStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UploadedBy { get; set; }
    }
}
