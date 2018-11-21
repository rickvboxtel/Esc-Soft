using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Puzzle
{
    public class GetPuzzleState : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            return (double)cache.GetOrAdd("puzzle_state", () => 0);
        }
    }
}