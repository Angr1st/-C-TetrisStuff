using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  class GameGridEventArgs:EventArgs
    {

    internal GameGridEventArgs( GameTile[,] internalGameGrid, List<GameTile> changedGameTiles)
      {
      GameGrid = internalGameGrid;
      ChangedGameTiles = new List<GameTile>( changedGameTiles );
      }

    public List<GameTile> ChangedGameTiles { get; }
    public GameTile[,] GameGrid { get; }
    }
  }
