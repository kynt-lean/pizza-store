namespace PizzaStore.Contracts;

public class OrderWithStatusDto
{
    public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(5);
    public readonly static TimeSpan DeliveryDuration = TimeSpan.FromSeconds(10);

    public OrderDto Order { get; set; } = null!;

    public string StatusText { get; set; } = null!;

    public bool IsDelivered => StatusText == "Delivered";

    public List<Marker> MapMarkers { get; set; } = null!;

    public static OrderWithStatusDto FromOrder(OrderDto order)
    {
        if (order.DeliveryLocation == null)
        {
            throw new ArgumentNullException(nameof(order.DeliveryLocation));
        }
        // To simulate a real backend process, we fake status updates based on the amount of time since the order was placed
        string statusText;
        List<Marker> mapMarkers;
        var dispatchTime = order.CreatedTime.Add(PreparationDuration);

        if (DateTime.Now < dispatchTime)
        {
            statusText = "Preparing";
            mapMarkers = [ToMapMarker("You", order.DeliveryLocation, showPopup: true)];
        }
        else if (DateTime.Now < dispatchTime + DeliveryDuration)
        {
            statusText = "Out for delivery";

            var startPosition = ComputeStartPosition(order);
            var proportionOfDeliveryCompleted = Math.Min(1, (DateTime.Now - dispatchTime).TotalMilliseconds / DeliveryDuration.TotalMilliseconds);
            var driverPosition = LatLongDto.Interpolate(startPosition, order.DeliveryLocation, proportionOfDeliveryCompleted);
            mapMarkers = [
                ToMapMarker("You", order.DeliveryLocation),
                ToMapMarker("Driver", driverPosition, showPopup: true),
            ];
        }
        else
        {
            statusText = "Delivered";
            mapMarkers = [ToMapMarker("Delivery location", order.DeliveryLocation, showPopup: true)];
        }

        return new OrderWithStatusDto
        {
            Order = order,
            StatusText = statusText,
            MapMarkers = mapMarkers,
        };
    }

    private static LatLongDto ComputeStartPosition(OrderDto order)
    {
        if (order.DeliveryLocation == null)
        {
            throw new ArgumentNullException(nameof(order.DeliveryLocation));
        }
        var rng = new Random(order.OrderId); // Random but deterministic based on order ID
        var distance = 0.01 + rng.NextDouble() * 0.02;
        var angle = rng.NextDouble() * Math.PI * 2;
        var offset = (distance * Math.Cos(angle), distance * Math.Sin(angle));
        return new LatLongDto(order.DeliveryLocation.Latitude + offset.Item1, order.DeliveryLocation.Longitude + offset.Item2);
    }

    static Marker ToMapMarker(string description, LatLongDto coords, bool showPopup = false)
        => new()
        { Description = description, X = coords.Longitude, Y = coords.Latitude, ShowPopup = showPopup };
}