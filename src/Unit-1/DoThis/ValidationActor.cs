using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor that validates user input and signal result to others.
    /// </summary>
    public class ValidationActor : UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;
        public ValidationActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }
        protected override void OnReceive(object message)
        {
            var msg = message as string;
            if (string.IsNullOrEmpty(msg))
            {
                //signal that the user needs to supply an input
                _consoleWriterActor.Tell(new Messages.NullInputError("No input received."));
            }
            else 
            {
                bool valid = IsValid(msg);
                if (valid)
                {
                    // send success to console writer
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid."));
                }
                else
                {
                    // send that input was bad
                    _consoleWriterActor.Tell(new Messages.ValidationError("Invalid: input had odd number of characters."));
                }
            }

            // tell sender to continue doing its thing
            // (whatever that may be, this actor doesn't care)
            Sender.Tell(new Messages.ContinueProcessing());
        }

        /// <sumary>
        /// Determines if the message received ir valid.
        /// Checks if number of chars in message received is even.
        /// </sumary>
        /// <param name="msg"></param>
        /// <returns> Boolean </returns>
        private static bool IsValid(string message)
        {
            bool valid = message.Length % 2 == 0;
            return valid;
        }
    }
}
