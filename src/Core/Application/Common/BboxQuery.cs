namespace Core.Application.Common;

public class BboxQuery
{
    public double MinLng { get; set; }

    public double MinLat { get; set; }

    public double MaxLng { get; set; }

    public double MaxLat { get; set; }

    public int? Zoom { get; set; }

    public void Validate()
    {
        if (MinLng < -180 || MaxLng > 180 || MinLat < -90 || MaxLat > 90)
            throw new ArgumentException("Invalid bbox coordinates.");

        if (MinLng >= MaxLng || MinLat >= MaxLat)
            throw new ArgumentException("Invalid bbox range.");
    }
}
