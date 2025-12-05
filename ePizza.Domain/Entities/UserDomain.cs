
namespace ePizza.Domain.Entities
{
    public class UserDomain
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        public List<string> UserRoles { get; set; } = default!;
    }
}
