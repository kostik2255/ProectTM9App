namespace ProectTM9Api.Models
{
    public class ConsultationRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Question { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsCompleted { get; set; } // Поле для статуса заявки
    }
}
