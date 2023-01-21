using Bogus;
using HomeBase.Dtos;

namespace HomeBase.Fakes;

public class FakeTelemetryPoint : Faker<TelemetryPoint>
{
    public FakeTelemetryPoint()
    {
        this.RuleFor(p => p.Timestamp, f => f.Date.SoonOffset());
        this.RuleFor(p => p.AccelerationX, f => f.Random.Double());
        this.RuleFor(p => p.AccelerationY, f => f.Random.Double());
        this.RuleFor(p => p.AccelerationZ, f => f.Random.Double());
    }
}
