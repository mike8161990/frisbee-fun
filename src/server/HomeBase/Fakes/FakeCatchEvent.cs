using Bogus;
using HomeBase.Dtos;

namespace HomeBase.Fakes;

public class FakeCatchEvent : Faker<CatchEvent>
{
    public FakeCatchEvent()
    {
        this.Rules((f, c) =>
        {
            var startTimestamp = DateTimeOffset.UtcNow;
            var startX = f.Random.Double();
            var startY = f.Random.Double();
            var startZ = f.Random.Double();

            c.Timestamp = startTimestamp;
            c.TelemetryPoints = Enumerable.Range(0, 30)
                .Select(r => new TelemetryPoint
                {
                    Timestamp = startTimestamp -= TimeSpan.FromMilliseconds(f.Random.Number(300)),
                    AccelerationX = startX += f.Random.Double(-.1, .1),
                    AccelerationY = startY += f.Random.Double(-.1, .1),
                    AccelerationZ = startZ += f.Random.Double(-.1, .1),
                })
                .ToList();
        });
    }
}
