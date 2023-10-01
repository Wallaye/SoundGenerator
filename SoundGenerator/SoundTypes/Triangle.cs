using System;

namespace SoundGenerator.SoundTypes;

public class Triangle : ISound
{
    public string Name { get; set; } = "Triangle";
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double Generate(double tick)
    {
        return 2 * Amplitude / Math.PI * Math.Asin(Math.Sin(2 * Math.PI * Frequency * tick + Offset));
    }
    public override string ToString()
    {
        return $"Triangle {Frequency} {Amplitude} {Offset}";
    }
}