using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class TetronimoMovementEventArgs: EventArgs
    {
    internal TetronimoMovementEventArgs(TetronimoMovement move)
      {
      TetronimoMove = move;
      }
    public TetronimoMovement TetronimoMove { get; }
    }
  }