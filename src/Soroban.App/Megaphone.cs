using System.Globalization;
using System.Speech.Synthesis;

namespace Soroban.App
{
    public class Megaphone : IOutput
    {
        private readonly CultureInfo _culture;

        public Megaphone(CultureInfo culture)
        {
            _culture = culture;
        }

        public void Write(object line)
        {
            var speechSynthesizer = new SpeechSynthesizer();
            var builder = new PromptBuilder(_culture);
            builder.AppendText(line.ToString());

            speechSynthesizer.Speak(builder);
        }
    }
}