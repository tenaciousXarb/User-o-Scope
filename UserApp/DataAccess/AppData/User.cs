namespace UserApp.DataAccess.AppData;

public partial class User : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;
}
