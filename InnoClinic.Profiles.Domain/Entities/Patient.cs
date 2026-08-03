

namespace InnoClinic.Profiles.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public bool IsLinkedToAccount { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Guid? AccountId { get; set; }

    }
}
