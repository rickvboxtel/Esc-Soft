using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Puzzle
{
    public class GetPuzzleTime : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            if (cache.Get<DateTime>("puzzle_time") != null)
            {
                return (DateTime.Now - cache.Get<DateTime>("puzzle_time")).TotalMinutes;
            }
            else
            {
                cache.Add<DateTime>("puzzle_time", DateTime.Now);
                return 0;
            }
        }
    }
}