using System;

namespace SoundGenerator.SoundTypes;

public class Noise : ISound
{
    public string Name { get; set; } = "Noise";
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; } = 0;
    public double Sum { get; set; } = 0;

    private Random Random { get; set; } = new Random();
    
    public double Generate(double tick)
    {
        return Amplitude * (2 * Random.NextDouble() - 1);
    }

    public double ModulateAmplitude(double tick, double signal)
    {
        throw new NotImplementedException();
    }

    public double ModulateFrequency(double tick, int n, double signal)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"Noise {Amplitude}";
    }
}