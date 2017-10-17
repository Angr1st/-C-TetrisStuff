using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class TickProcessEventArgs:EventArgs
    {
    internal TickProcessEventArgs( GameGrid gameGrid, GameStates gameState )
      {
      GameGrid = gameGrid;
      GameState = gameState;
      }
    public GameGrid  GameGrid{get;}
    public GameStates GameState { get; }

    }
  }