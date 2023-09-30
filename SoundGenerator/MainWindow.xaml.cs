using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SoundGenerator.SoundTypes;
using Point = System.Drawing.Point;

namespace SoundGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGenerateWav_OnClick(object sender, RoutedEventArgs e)
        {
            Noise signal1 = new Noise() { Amplitude = 1 };
            Triangle signal2 = new Triangle() { Amplitude = 1, Frequency = 220, Offset = 0 };
            SawTooth signal3 = new SawTooth() { Amplitude = 1, Frequency = 220, Offset = 0 };
            Sinusoid signal4 = new Sinusoid() { Amplitude = 1, Frequency = 220, Offset = 0 };
            Impulse signal5 = new Impulse() { Amplitude = 1, Frequency = 220, DutyCycle = 0.5, Offset = 0 };
            ISound[] sounds = new ISound[] { signal4 };
            double[] data = WavGenerator.CreateWav(sounds);
            DrawGraphic(data);
        }

        private void DrawGraphic(double[] data)
        {
            CnvGraphics.Children.Clear();
            int expandRate = 50;
            for (int i = 0; i < data.Length / 100; i += 10)
            {
                Line line = new Line();
                line.X1 = (double)i / data.Length * 100 * CnvGraphics.ActualWidth;
                line.X2 = (double)(i + 10) / data.Length * 100 * CnvGraphics.ActualWidth;
                line.Y1 = data[i] * expandRate + CnvGraphics.ActualHeight / 2;
                line.Y2 = data[i + 10] * expandRate + CnvGraphics.ActualHeight / 2;
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                CnvGraphics.Children.Add(line);
            }
        }
    }
}