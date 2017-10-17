using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
 internal class GameStateEventArgs:EventArgs
    {
   internal GameStateEventArgs(GameStates state)
     {
     GameState = state;
     }

   public GameStates GameState { get; set; }
    }
  }
