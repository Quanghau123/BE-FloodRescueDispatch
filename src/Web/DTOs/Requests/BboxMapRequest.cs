namespace Web.DTOs.Requests;

public sealed class BboxMapRequest
{
    public double MinLng { get; set; }

    public double MinLat { get; set; }

    public double MaxLng { get; set; }

    public double MaxLat { get; set; }

    public int? Zoom { get; set; }
}

