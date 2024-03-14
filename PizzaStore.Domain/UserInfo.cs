namespace PizzaStore.Domain;

public class UserInfo
{
    public bool IsAuthenticated { get; set; }

    public string Name { get; set; } = string.Empty;
}