using System;

namespace SoundGenerator.SoundTypes;

public class SawTooth : ISound
{
    public string Name { get; set; } = "SawTooth";
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double Sum { get; set; } = 0;
    
    public double Generate(double tick)
    {
        return -2 * Amplitude / Math.PI * Math.Atan(1 / Math.Tan(Math.PI * Frequency * tick + Offset));
    }

    public double ModulateAmplitude(double tick, double signal)
    {
        return Generate(tick) * (1 + signal);
    }

    public double ModulateFrequency(int n, double signal)
    {
        Sum += 1 + signal;
        return -2 * Amplitude / Math.PI * Math.Atan(1 / Math.Tan(Math.PI * Frequency * Sum / n + Offset));
    }


    public override string ToString()
    {
        return $"SawTooth {Frequency} {Amplitude} {Offset}";
    }
}