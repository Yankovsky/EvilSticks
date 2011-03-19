﻿using Game;

namespace EvilSticks.Model
{
    public sealed class SticksGame : TwoPlayersTurnBasedGame
    {
        public SticksGame(int sticksCount, int currentPlayerIndex, Player firstPlayer, Player secondPlayer)
            : base(currentPlayerIndex, firstPlayer, secondPlayer)
        {
            SticksCount = sticksCount;
        }

        public int SticksCount { get; private set; }

        protected override void UpdateGameState(object move)
        {
            SticksCount -= (int)move;
            if (SticksCount <= 0)
            {
                FirstPlayer.State = IsFirstPlayerTurn ? PlayerInGameState.Win : PlayerInGameState.Lose;
                SecondPlayer.State = IsFirstPlayerTurn ? PlayerInGameState.Lose : PlayerInGameState.Win;
            }
        }

        protected override void OnGameEnding() { }
    }
}