using Bogus;
using HomeBase.Dtos;

namespace HomeBase.Fakes;

public class FakeCatchEvent : Faker<CatchEvent>
{
    public FakeCatchEvent()
    {
        this.Rules((f, c) =>
        {
            var now = DateTimeOffset.UtcNow;
            c.Timestamp = now;
            c.TelemetryPoints = new FakeTelemetryPoint()
                .RuleFor(p => p.Timestamp, f => now -= TimeSpan.FromMilliseconds(100))
                .Generate(30);
        });
    }
}
