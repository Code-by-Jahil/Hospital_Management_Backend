namespace Hospital_Management.DTO
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string DayName { get; set; }
        public string AppointmentType { get; set; }
        public string Status { get; set; } 
        public string Notes { get; set; }
    }

    public class AppointmentUpdateDto
    {
        public int AppointmentId { get; set; }
        public string AppointmentType { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string DayName { get; set; }
    }

    public class AppointmentViewDto
    {
        public int Appointment_Id { get; set; }
        public DateTime Appointment_Date { get; set; }
        public TimeSpan Appointment_Time { get; set; }
        public string Day_Name { get; set; }
        public string Appointment_Type { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
