using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoundGenerator;

public class WavGenerator
{
    public const int SampleRate = 44100;
    public const int Seconds = 2;
    public const int NumSamples = SampleRate * Seconds;

    private static byte[] CreateWavHeader()
    {
        List<byte> result = new();
        short bitsPerSample = 16;
        short numChannels = 1;
        short frameSize = (short)(numChannels * bitsPerSample / 8);
        int bytesPerSecond = SampleRate * frameSize;
        int dataChunkSize = NumSamples * frameSize;
        int fileSize = 36 + dataChunkSize;
        result.AddRange(Encoding.ASCII.GetBytes("RIFF"));
        result.AddRange(BitConverter.GetBytes(fileSize));
        result.AddRange(Encoding.ASCII.GetBytes("WAVE"));
        result.AddRange(Encoding.ASCII.GetBytes("fmt "));
        result.AddRange(BitConverter.GetBytes(16));
        result.AddRange(BitConverter.GetBytes((short)1));
        result.AddRange(BitConverter.GetBytes(numChannels));
        result.AddRange(BitConverter.GetBytes(SampleRate));
        result.AddRange(BitConverter.GetBytes(bytesPerSecond));
        result.AddRange(BitConverter.GetBytes(frameSize));
        result.AddRange(BitConverter.GetBytes(bitsPerSample));
        result.AddRange(Encoding.ASCII.GetBytes("data"));
        result.AddRange(BitConverter.GetBytes(dataChunkSize));
        return result.ToArray();
    }

    private static double[] GenerateWavData(ISound sound)
    {
        double[] data = new double[NumSamples];
        for (int i = 0; i < NumSamples; i++)
        {
            double tick = (double)i / SampleRate;
            data[i] = sound.Generate(tick);
        }

        return data;
    }

    public static double[] CreateWav(ISound[] sounds)
    {
        double[] data = new double[NumSamples];
        foreach (var sound in sounds)
        {
            double[] temp = GenerateWavData(sound);
            for (int i = 0; i < NumSamples; i++)
            {
                data[i] += temp[i];
            }
        }
        return data;
    }

    public static void PlayWav(double[] data)
    {
        using var memoryStream = new MemoryStream();
        using var bw = new BinaryWriter(memoryStream);

        var header = CreateWavHeader();
        bw.Write(header);
        foreach (var sample in data)
        {
            short sampleValue = (short)(sample * short.MaxValue);
            bw.Write(sampleValue);
        }

        memoryStream.Seek(0, SeekOrigin.Begin);
        new System.Media.SoundPlayer(memoryStream).Play();
    }

    public static double[] ModulateAmplitude(ISound carried, ISound modulating)
    {
        double[] result = new double[NumSamples];
        for (int i = 0; i < NumSamples; i++)
        {
            double tick = (double)i / SampleRate;
            double modulatingVal = modulating.Generate(tick);
            result[i] = carried.ModulateAmplitude(tick, modulatingVal);
        }
        return result;
    }
    
    public static double[] ModulateFrequency(ISound carried, ISound modulating)
    {
        double[] result = new double[NumSamples];
        carried.Sum = 0;
        for (int i = 0; i < NumSamples; i++)
        {
            double tick = (double)i / SampleRate;
            double modulatingVal = modulating.Generate(tick);
            result[i] = carried.ModulateFrequency(tick, SampleRate, modulatingVal);
        }
        return result;
    }
}