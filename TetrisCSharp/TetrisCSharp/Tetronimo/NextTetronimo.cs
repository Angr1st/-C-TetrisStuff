using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  class NextTetronimo
    {
    private Random m_Random;
    public NextTetronimo()
      {
      m_Random = new Random((int)DateTime.Now.Ticks );
      }

    public Tetronimo GetNextTetronimo()
      {
      Tetronimo returnValue;
      switch ( m_Random.Next( 1, 8 ) )
        {
        case 1:
          returnValue = new Tetronimo( TetronimoType.I );
          break;

        case 2:
          returnValue = new Tetronimo( TetronimoType.J );
          break;

        case 3:
          returnValue = new Tetronimo( TetronimoType.L );
          break;

        case 4:
          returnValue = new Tetronimo( TetronimoType.O );
          break;

        case 5:
          returnValue = new Tetronimo( TetronimoType.S );
          break;

        case 6:
          returnValue = new Tetronimo( TetronimoType.T );
          break;

        case 7:
          returnValue = new Tetronimo( TetronimoType.Z );
          break;

        default:
          returnValue = new Tetronimo( TetronimoType.Unknown );
          break;
        }

      return returnValue;
      }
    }
  }
