namespace SoundGenerator;

public interface ISound
{
    public double Frequency { get; set; }
    public double Amplitude { get; set; }
    public double Offset { get; set; }
    public double Generate(double tick);
}