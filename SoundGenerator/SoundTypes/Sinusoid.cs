using System;

namespace SoundGenerator.SoundTypes;

public class Sinusoid : ISound  
{
    public string Name { get; set; } = "Sinusoide";
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double Sum { get; set; } = 0;

    public double Generate(double tick)
    {
        return Amplitude * Math.Sin(2.0 * Math.PI * Frequency * tick + Offset);
    }

    public double ModulateAmplitude(double tick, double signal)
    {
        return Generate(tick) * (1 + signal);
    }

    public double ModulateFrequency(double tick, int n, double signal)
    {
        Sum += 1 + signal;
        return Amplitude * Math.Sin(2 * Math.PI * (double)Sum / n + Offset);
    }

    public override string ToString()
    {
        return $"Sinosuide {Frequency} {Amplitude} {Offset}";
    }
}