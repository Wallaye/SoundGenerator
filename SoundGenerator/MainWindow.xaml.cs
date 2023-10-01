using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

namespace SoundGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ISound> Sounds = new ObservableCollection<ISound>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SignalListBox.ItemsSource = Sounds;
        }

        private bool CheckDataInput() {
            if (!double.TryParse(AmplitudeTextBox.Text, out _))
            {
                return false;
            }
            if (SignalTypeComboBox.SelectedIndex == 4)
            {
                return true;
            }
            if (!double.TryParse(FrequencyTextBox.Text, out _))
            {
                return false;
            }
            if (!double.TryParse(OffsetTextBox.Text, out _))
            {
                return false;
            }
            if (SignalTypeComboBox.SelectedIndex != 1)
            {
                return true;
            }
            if (!double.TryParse(DutyCycleTextBox.Text, out _))
            {
                return false;
            }
            return true;
        }

        private void BtnGenerateWav_OnClick(object sender, RoutedEventArgs e)
        {
            double[] data = WavGenerator.CreateAndPlayWav(Sounds.ToArray());
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

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDataInput())
            {
                return;
            }
            ISound signal;
            double.TryParse(AmplitudeTextBox.Text, out double Amplitude);
            double.TryParse(FrequencyTextBox.Text, out double Frequency);
            double.TryParse(OffsetTextBox.Text, out double Offset);
            double.TryParse(DutyCycleTextBox.Text, out double DutyCycle);
            switch (SignalTypeComboBox.SelectedIndex)
            {
                case 0:
                    signal = new Sinusoid { Amplitude = Amplitude, Frequency = Frequency, Offset = Offset };
                    break;
                case 1:
                    signal = new Impulse { Amplitude = Amplitude, Frequency = Frequency, Offset = Offset, DutyCycle = DutyCycle};
                    break;
                case 2:
                    signal = new Triangle { Amplitude = Amplitude, Frequency = Frequency, Offset = Offset };
                    break;
                case 3:
                    signal = new SawTooth { Amplitude = Amplitude, Frequency = Frequency, Offset = Offset };
                    break;
                case 4:
                    signal = new Noise { Amplitude = Amplitude };
                    break;
                default:
                    signal = new Sinusoid { Amplitude = 1, Frequency = 220, Offset = 0 };
                    break;
            }
            signal.Name = signal.ToString();
            Sounds.Add(signal);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ISound signalToDelete = (ISound)button.Tag;

            if (signalToDelete != null)
            {
                Sounds.Remove(signalToDelete);
            }
        }

        private void SignalListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SignalListBox.SelectedItems != null)
            {
                ISound selectedSound = (ISound)SignalListBox.SelectedItem;
                AmplitudeTextBox.Text = selectedSound.Amplitude.ToString();
                FrequencyTextBox.Text = selectedSound.Frequency.ToString();
                OffsetTextBox.Text = selectedSound.Offset.ToString();
                var s = selectedSound as Impulse;
                if (s != null)
                {
                    DutyCycleTextBox.Text = s.DutyCycle.ToString();
                }
            }
        }
    }
}