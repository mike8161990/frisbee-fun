namespace HomeBase.Dtos;

public class TelemetryPoint
{
    public DateTimeOffset Timestamp { get; set; }

    public double AccelerationX { get; set; }

    public double AccelerationY { get; set; }

    public double AccelerationZ { get; set; }
}
