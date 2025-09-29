public class User
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RolesEnum Roles { get; set; }
}