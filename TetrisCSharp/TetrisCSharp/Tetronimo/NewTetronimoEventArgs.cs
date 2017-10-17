using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetrisCSharp
  {
  internal class NewTetronimoEventArgs:EventArgs
    {
    internal NewTetronimoEventArgs(Tetronimo newTetronimo)
      {
      NewTetronimo = newTetronimo;
      }
    public Tetronimo NewTetronimo { get; }
    }
  }