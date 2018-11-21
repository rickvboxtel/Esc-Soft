using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Puzzle
{
    public class PuzzleStateUp : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            cache.Add<int>("puzzle_state", cache.GetOrAdd("puzzle_state", () => 0)+1);
            cache.Add("puzzle_time", DateTime.Now);
            return true;
        }
    }
}