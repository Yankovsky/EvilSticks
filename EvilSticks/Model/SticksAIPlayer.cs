using System.ComponentModel;
using System.Threading;
using Game;
using System;
using Tools;
using System.Collections.Generic;

namespace EvilSticks.Model
{
    public sealed class SticksAIPlayer : AIPlayer
    {
        public SticksAIPlayer(string name, int latency) : base(name, latency)
        {
            var init = 100;
            var sticksCount = 11;
            _boxes = new List<List<int>>();
            for (int i = 0; i < sticksCount; i++)
            {
                _boxes.Add(new List<int>());
                for (int j = 0; j < init / 2; j++)
                {
                    _boxes[i].Add(1);
                    _boxes[i].Add(2);
                }
            }
        }

        private void Initialize()
        {
            _movesIndexHistory = new Dictionary<int, int>();
            Game.GameEnded += OnGameEnded;
        }

        private void OnGameEnded(object sender, GameEndedEventArgs e)
        {
            foreach (var boxNumber in _movesIndexHistory.Keys)
            {
                if (this != Game.CurrentPlayer)
                {
                    var moveIndex = _movesIndexHistory[boxNumber];
                    var rightValue = _boxes[boxNumber][moveIndex] == 1 ? 2 : 1;
                    _boxes[boxNumber][moveIndex] = rightValue;
                }
            }
        }

        private List<List<int>> _boxes;
        private Dictionary<int, int> _movesIndexHistory;

        public override void MakeMove()
        {
            /*
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler((sender2, e2) =>
            {
                Thread.Sleep(Latency);
            });
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler((sender2, e2) =>
            {
                var sticksCount = ((SticksGame)Game).SticksCount;
                var moveIndex = DataHelper.GetRandomIndex(_boxes[sticksCount - 1]);
                _movesIndexHistory[sticksCount - 1] = moveIndex;
                OnMoveMade(_boxes[sticksCount - 1][moveIndex]);
            });
            worker.RunWorkerAsync();
             * */

            Thread.Sleep(Latency);
            var sticksCount = ((SticksGame)Game).SticksCount;
            var moveIndex = DataHelper.GetRandomIndex(_boxes[sticksCount - 1]);
            _movesIndexHistory[sticksCount - 1] = moveIndex;
            OnMoveMade(_boxes[sticksCount - 1][moveIndex]);
        }

        protected override void OnGameStarted(object sender, EventArgs e)
        {
            Initialize();
        }

    }
}