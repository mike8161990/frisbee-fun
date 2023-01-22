namespace HomeBase.Dtos;

public class TelemetryPointSubmission
{
    public long Timestamp { get; set; }

    public double AccelerationX { get; set; }

    public double AccelerationY { get; set; }

    public double AccelerationZ { get; set; }
}
