using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockly.Blocks.Text
{
    public class ColourPicker : IBlock
    {
        public override object Evaluate(Context context)
        {
            return this.Fields.Get("COLOUR") ?? "#000000";
        }
    }

}