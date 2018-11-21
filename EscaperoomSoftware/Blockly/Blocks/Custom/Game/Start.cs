using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Game
{
    public class Start : IBlock
    {
        public override object Evaluate(Context context)
        {
            Console.WriteLine("in Start");
            IAppCache cache = new CachingService();
            cache.Add("game_time", DateTime.Now);
            cache.Add("puzzle_time", DateTime.Now);
            Console.WriteLine("start");
            return base.Evaluate(context);
        }
    }
}