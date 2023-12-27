namespace Academy.Model
{
    public class Payment
    {
        public string StudentName { get; set; }

        public int RegistrationId { get; set; }

        public string CourseDetail { get; set; }

        public string BatchDetail { get; set; }

        public string TraineeName { get; set; }

        public int TotalFees { get; set; }

        public int No_Of_Installment { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime DateOfPayment { get; set; }



    }
}
