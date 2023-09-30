using System;

namespace SoundGenerator.SoundTypes;

public class SawTooth : ISound
{
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double Generate(double tick)
    {
        return -2 * Amplitude / Math.PI * Math.Atan(1 / Math.Tan(Math.PI * Frequency * tick + Offset));
    }
}