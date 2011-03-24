using System.Collections.Generic;
using System.Linq;
using Game;
using Tools;

namespace EvilSticks.Model
{
    public sealed class SticksAIPlayer : AIPlayer
    {
        #region Public Interface

        public SticksAIPlayer(string name, int latency) : base(name, latency)
        {
            var initialValue = 10;
            _boxes = new List<List<int>>();
            for (int i = 0; i < _sticksCount; i++)
            {
                _boxes.Add(new List<int>());
                for (int j = 0; j < initialValue; j++)
                {
                    _boxes[i].Add(1);
                    _boxes[i].Add(2);
                }
                /*
          ______________________________
         |      \                      |
         |       \ boxes |0|1|2| ...|10|
         |_index__\______|_|_|_|_...|__|
         |___0____|______|1|1|1| ...|1_|
         |___1____|______|2|2|2| ...|2_|
         |___2____|______|1|1|1| ...|1_|
         |___3____|______|2|2|2| ...|2_|
         |___4____|______|1|1|1| ...|1_|
         |___5____|______|2|2|2| ...|2_|
         | ...            . . .  ...
         |___19___|______|2|2|2| ...|2_|
         |________|______|_|_|_|_...|__|     
                 
                 */
            }
        }

        #endregion

        #region Override

        protected override object MakingMove()
        {
            var boxNumber = ((SticksGame)Game).SticksCount - 1;
            var moveIndex = _boxes[boxNumber].GetRandomIndex();
            _movesIndexHistory[boxNumber] = moveIndex;
            return _boxes[boxNumber][moveIndex];
        }

        protected override void GameStarted()
        {
            _movesIndexHistory = new Dictionary<int, int>();
        }

        protected override void GameOver()
        {
            foreach (var boxNumber in _movesIndexHistory.Keys)
            {
                var moveIndex = _movesIndexHistory[boxNumber];
                var move = _boxes[boxNumber][moveIndex];
                var invertMove = move == 1 ? 2 : 1;

                if (this == Game.CurrentPlayer) // CurrentPlayer == Winner
                {
                    if (_boxes[boxNumber].Contains(invertMove))
                    {
                        var wrongMoveIndex = _boxes[boxNumber].IndexOf(invertMove);
                        _boxes[boxNumber][wrongMoveIndex] = move;
                    }
                }
                else
                {
                    _boxes[boxNumber][moveIndex] = invertMove;
                }
            }
        }

        #endregion

        #region Private Members

        private int _sticksCount = 11;
        private List<List<int>> _boxes;
        private Dictionary<int, int> _movesIndexHistory;

        #endregion

    }
}