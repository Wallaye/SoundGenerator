namespace SoundGenerator.SoundTypes;

public class Impulse : ISound
{
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double DutyCycle { get; set; }
    public double Generate(double tick)
    {
        double T = 1 / Frequency;
        return (tick % T) / T < DutyCycle ? Amplitude : -Amplitude;
    }
}