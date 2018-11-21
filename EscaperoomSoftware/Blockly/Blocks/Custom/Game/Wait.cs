using LazyCache;
using System;
using System.Threading;

namespace Blockly.Blocks.Game
{
    public class Wait : IBlock
    {
        public override object Evaluate(Context context)
        {
            IAppCache cache = new CachingService();
            Console.WriteLine("in Wait");
            double sleepTimeSeconds = (double)Values.Evaluate("seconds", context);
            for (int i = 0; i < sleepTimeSeconds*10; i++)
            {
                if (cache.GetOrAdd("stop", () => false) == true)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            return base.Evaluate(context);
        }
    }
}