namespace PizzaStore.Contracts.Map;

public class MarkerDto
{
    public string Description { get; set; } = string.Empty;

    public double X { get; set; }

    public double Y { get; set; }

    public bool ShowPopup { get; set; }
}