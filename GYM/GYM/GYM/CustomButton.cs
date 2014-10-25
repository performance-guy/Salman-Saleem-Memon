using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace GYM
{
    class CustomButton : Button
    {
        public CustomButton()
        { SetStyle(ControlStyles.Selectable, false); }
        public new void PerformClick() { this.OnClick(EventArgs.Empty); }
    }
}