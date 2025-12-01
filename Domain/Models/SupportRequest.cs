namespace CaseStrategy.Domain.Models
{
    public class SupportRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }     
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class ProcessedSupportRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public string Handler { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
        public string Priority { get; set; }
    }    
}

