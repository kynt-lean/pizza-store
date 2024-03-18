namespace PizzaStore.Contracts;

public class LatLongDto
{
    public LatLongDto()
    {
    }

    public LatLongDto(double latitude, double longitude) : this()
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public static LatLongDto Interpolate(LatLongDto start, LatLongDto end, double proportion)
    {
        // The Earth is flat, right? So no need for spherical interpolation.
        return new LatLongDto(
                start.Latitude + (end.Latitude - start.Latitude) * proportion,
                start.Longitude + (end.Longitude - start.Longitude) * proportion);
    }
}