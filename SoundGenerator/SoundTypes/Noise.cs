using System;

namespace SoundGenerator.SoundTypes;

public class Noise : ISound
{
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    private Random Random { get; set; } = new Random();
    public double Generate(double tick)
    {
        return Amplitude * (2 * Random.NextDouble() - 1);
    }
}