using SoundGenerator.SoundTypes;
using System;
using System.CodeDom;
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
using System.Windows.Shapes;

namespace SoundGenerator
{
    /// <summary>
    /// Логика взаимодействия для ModulationWindow.xaml
    /// </summary>
    public partial class ModulationWindow : Window
    {
        public ModulationWindow()
        {
            InitializeComponent();
        }

        private bool CheckDataInput(ComboBox box, TextBox[] boxes)
        {
            if (!double.TryParse(boxes[0].Text, out _))
            {
                return false;
            }
            if (box.SelectedIndex == 4)
            {
                return true;
            }
            if (!double.TryParse(boxes[1].Text, out _))
            {
                return false;
            }
            if (!double.TryParse(boxes[2].Text, out _))
            {
                return false;
            }
            if (box.SelectedIndex != 1)
            {
                return true;
            }
            if (!double.TryParse(boxes[3].Text, out _))
            {
                return false;
            }
            return true;
        }

        private void BtnAmpl_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDataInput(SignalTypeComboBox1, new TextBox[] 
                { Signal1AmplitudeTextBox, 
                    Signal1FrequencyTextBox, 
                    Signal1OffsetTextBox, 
                    Signal1DutyCycleTextBox} ) 
                    || !CheckDataInput(SignalTypeComboBox2, new TextBox[]
                { Signal2AmplitudeTextBox,
                    Signal2FrequencyTextBox,
                    Signal2OffsetTextBox,
                    Signal2DutyCycleTextBox}))
            {
                return;
            }
            ISound[] Sounds = GetSignals();
            double[] data = WavGenerator.ModulateAmplitude(Sounds[0], Sounds[1]);
            WavGenerator.PlayWav(data);
        }

        private ISound[] GetSignals()
        {
            ISound signal1, signal2;
            double.TryParse(Signal1AmplitudeTextBox.Text, out double Amplitude1);
            double.TryParse(Signal1FrequencyTextBox.Text, out double Frequency1);
            double.TryParse(Signal1OffsetTextBox.Text, out double Offset1);
            double.TryParse(Signal1DutyCycleTextBox.Text, out double DutyCycle1);
            double.TryParse(Signal2AmplitudeTextBox.Text, out double Amplitude2);
            double.TryParse(Signal2FrequencyTextBox.Text, out double Frequency2);
            double.TryParse(Signal2OffsetTextBox.Text, out double Offset2);
            double.TryParse(Signal2DutyCycleTextBox.Text, out double DutyCycle2);
            switch (SignalTypeComboBox1.SelectedIndex)
            {
                case 0:
                    signal1 = new Sinusoid { Amplitude = Amplitude1, Frequency = Frequency1, Offset = Offset1 };
                    break;
                case 1:
                    signal1 = new Impulse { Amplitude = Amplitude1, Frequency = Frequency1, Offset = Offset1, DutyCycle = DutyCycle1 };
                    break;
                case 2:
                    signal1 = new Triangle { Amplitude = Amplitude1, Frequency = Frequency1, Offset = Offset1 };
                    break;
                case 3:
                    signal1 = new SawTooth { Amplitude = Amplitude1, Frequency = Frequency1, Offset = Offset1 };
                    break;
                case 4:
                    signal1 = new Noise { Amplitude = Amplitude1 };
                    break;
                default:
                    signal1 = new Sinusoid { Amplitude = 1, Frequency = 220, Offset = 0 };
                    break;
            }
            switch (SignalTypeComboBox2.SelectedIndex)
            {
                case 0:
                    signal2 = new Sinusoid { Amplitude = Amplitude2, Frequency = Frequency2, Offset = Offset2 };
                    break;
                case 1:
                    signal2 = new Impulse { Amplitude = Amplitude2, Frequency = Frequency2, Offset = Offset2, DutyCycle = DutyCycle2 };
                    break;
                case 2:
                    signal2 = new Triangle { Amplitude = Amplitude2, Frequency = Frequency2, Offset = Offset2 };
                    break;
                case 3:
                    signal2 = new SawTooth { Amplitude = Amplitude2, Frequency = Frequency2, Offset = Offset2 };
                    break;
                case 4:
                    signal2 = new Noise { Amplitude = Amplitude2 };
                    break;
                default:
                    signal2 = new Sinusoid { Amplitude = 1, Frequency = 220, Offset = 0 };
                    break;
            }
            return new ISound[2] { signal1, signal2 };
        }

        private void BtnFreq_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckDataInput(SignalTypeComboBox1, new TextBox[]
                { Signal1AmplitudeTextBox,
                    Signal1FrequencyTextBox,
                    Signal1OffsetTextBox,
                    Signal1DutyCycleTextBox})
                    || !CheckDataInput(SignalTypeComboBox2, new TextBox[]
                { Signal2AmplitudeTextBox,
                    Signal2FrequencyTextBox,
                    Signal2OffsetTextBox,
                    Signal2DutyCycleTextBox}))
            {
                return;
            }
            ISound[] Sounds = GetSignals();
            double[] data = WavGenerator.ModulateFrequency(Sounds[0], Sounds[1]);
            WavGenerator.PlayWav(data);
        }
    }
}
