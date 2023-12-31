﻿namespace SoundGenerator;

public interface IModulating
{
    public double ModulateAmplitude(double tick, double signal);
    public double ModulateFrequency(int n, double signal);
    public double Sum { get; set; }
}