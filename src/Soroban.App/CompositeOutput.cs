using System;
using System.Collections.Generic;

namespace Soroban.App
{
    public class CompositeOutput : IOutput
    {
        private readonly IEnumerable<IOutput> _outputs;

        public CompositeOutput(params IOutput[] outputs)
        {
            if (outputs == null)
                throw new ArgumentNullException(nameof(outputs));
            _outputs = outputs;
        }

        public void Write(object line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            foreach (var output in _outputs)
            {
                output.Write(line);
            }
        }
    }
}