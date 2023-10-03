namespace SoundGenerator.SoundTypes;

public class Impulse : ISound
{
    public string Name { get; set; } = "Impulse";
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double DutyCycle { get; set; }
    public double Sum { get; set; } = 0;

    public double Generate(double tick)
    {
        double T = 1 / Frequency;
        return (tick % T) / T < DutyCycle ? Amplitude : -Amplitude;
    }

    public double ModulateAmplitude(double tick, double signal)
    {
        throw new System.NotImplementedException();
    }

    public double ModulateFrequency(double tick, int n, double signal)
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        return $"Impulse {Frequency} {Amplitude} {Offset} {DutyCycle}";
    }
}