using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyUnityApp.MyDatabindings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SpinnerAndLabel : ContentPage
	{
		public SpinnerAndLabel ()
		{
			InitializeComponent ();
		}
	}
}