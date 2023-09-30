using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SoundGenerator;

public class WavGenerator
{
    public const int SampleRate = 44100;
    public const int NumSamples = SampleRate * 5;

    private static byte[] CreateWavHeader()
    {
        List<byte> result = new();
        short bitsPerSample = 16;
        short numChannels = 1;
        short frameSize = (short)(numChannels * bitsPerSample / 8);
        int bytesPerSecond = SampleRate * frameSize;
        int dataChunkSize = NumSamples * frameSize;
        int fileSize = 36 + dataChunkSize;
        
        /*using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms);
        writer.Write(Encoding.ASCII.GetBytes("RIFF"));
        writer.Write(fileSize);
        writer.Write(Encoding.ASCII.GetBytes("WAVE"));
        writer.Write(Encoding.ASCII.GetBytes("fmt "));
        writer.Write(16); 
        writer.Write((short)1); 
        writer.Write(numChannels);
        writer.Write(SampleRate);
        writer.Write(bytesPerSecond); 
        writer.Write(frameSize);
        writer.Write(bitsPerSample);
        writer.Write(Encoding.ASCII.GetBytes("data"));
        writer.Write(dataChunkSize);*/
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
        using (var fileStream = new FileStream("file.wav", FileMode.Create))
        {
            memoryStream.CopyTo(fileStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);
        new System.Media.SoundPlayer(memoryStream).Play();
        return data;
    }
}