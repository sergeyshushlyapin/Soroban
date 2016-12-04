using System.Globalization;
using System.Speech.Synthesis;

namespace Soroban.App
{
    public class SpeechRecord : IOutput
    {
        private readonly string _phrase;
        private readonly CultureInfo _culture;

        public SpeechRecord(string phrase, CultureInfo culture)
        {
            _phrase = phrase;
            _culture = culture;
        }

        public void Write(object line)
        {
            var speechSynthesizer = new SpeechSynthesizer();
            var builder = new PromptBuilder(_culture);
            builder.AppendText(_phrase);

            speechSynthesizer.Speak(builder);
        }
    }
}