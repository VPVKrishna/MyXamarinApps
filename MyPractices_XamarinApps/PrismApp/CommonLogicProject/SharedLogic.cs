using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace CommonLogicProject
{
    public class SharedLogic
    {
        public static Color GetButtonColor()
        {

#if __IOS__
            return Color.Red;
#endif

#if __ANDROID__
            return Color.Green;
#endif
            return Color.Blue;
            
        }
    }
}
