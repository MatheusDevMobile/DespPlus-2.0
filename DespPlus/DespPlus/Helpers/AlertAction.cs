using System;
using System.Collections.Generic;
using System.Text;

namespace DespPlus.Helpers
{
    public class AlertAction
    {
        public string TextButton { get; }
        public Action Action { get; }

        public AlertAction(string textButton)
        {
            TextButton = textButton;
        }

        public AlertAction(string textButton, Action action) : this(textButton)
        {
            Action = action;
        }
    }
}
