using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Game
{
    public class PlayTime : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            if (cache.Get<DateTime>("game_time") != null)
            {
                return (DateTime.Now - cache.Get<DateTime>("game_time")).TotalMinutes;
            }
            else
            {
                cache.Add<DateTime>("game_time", DateTime.Now);
                return 0;
            }
        }
    }
}