using System;

namespace SoundGenerator.SoundTypes;

public class Sinusoid : ISound
{
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;

    public double Generate(double tick)
    {
        return Amplitude * Math.Sin(2.0 * Math.PI * Frequency * tick + Offset);
    }
}