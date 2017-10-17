using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class TetronimoRepresentationEventArgs:EventArgs
    {
    internal TetronimoRepresentationEventArgs(List<GameTile> tetronimo)
      {
      Tetronimo = new List<GameTile>( tetronimo );
      }
    public List<GameTile> Tetronimo { get; }
    }
  }