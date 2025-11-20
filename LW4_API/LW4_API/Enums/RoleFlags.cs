namespace LW4_API.Enums
{
    [Flags]
    public enum Roles
    {
        None = 0,
        User = 1 << 0, // 1
        Manager = 1 << 1, // 2
        Admin = 1 << 2, // 4
    }
}
