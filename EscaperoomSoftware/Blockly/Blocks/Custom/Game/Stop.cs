using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Game
{
    public class Stop : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            cache.Add("puzzle_state", 0);
            return base.Evaluate(context);
        }
    }
}