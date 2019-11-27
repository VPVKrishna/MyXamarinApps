using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PrismApp.Views
{
    public class NumberValidationTrigger : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            var len = sender.Text.Length;
            if(len <= 2)
            {
                sender.TextColor = Color.Yellow;
            }
            else
            {
                sender.TextColor = Color.Green;
            }
        }
    }
}
