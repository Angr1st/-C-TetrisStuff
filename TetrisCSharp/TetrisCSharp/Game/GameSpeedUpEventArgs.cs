using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class GameSpeedUpEventArgs
    {
    internal GameSpeedUpEventArgs(int newSpeed)
      {
      SpeedUp = newSpeed;
      }

    public int SpeedUp { get; }
    }
  }