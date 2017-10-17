using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class GameTileColorChangedEventArgs:EventArgs
    {
    internal GameTileColorChangedEventArgs(GameTile tile)
      {
      NewGameTile = tile;
      }

    public GameTile NewGameTile { get; }
    }
  }