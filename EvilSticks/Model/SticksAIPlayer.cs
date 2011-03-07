using System.ComponentModel;
using System.Threading;
using Game;

namespace EvilSticks.Model
{
    public sealed class SticksAIPlayer : AIPlayer
    {
        public SticksAIPlayer(string name, int latency) : base(name, latency) { }

        public int[] Boxes { get; private set; }

        public override void MakeMove()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender, e) =>
                {
                    Thread.Sleep(_latency);
                });
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender, e) =>
            {
                var a = ((SticksGame)Game).SticksCount;
                var move = 1;
                OnMoveMade(move);
            });
            worker.RunWorkerAsync();
        }

    }
}