using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisCSharp
  {
  public enum TetronimoType
    {
    I,
    J,
    L,
    O,
    S,
    T,
    Z,
    Unknown
    }

  class Tetronimo
    {
    private TetronimoType m_TetronimoType = TetronimoType.Unknown;
    private Point[] m_Form = null;
    private GameTileColors m_Color = GameTileColors.Schwarz;


    public Point[] Form
      {
      get { return new Point[4] { m_Form[0], m_Form[1], m_Form[2], m_Form[3] }; }
      }

    public TetronimoType TetronimoType
      {
      get { return m_TetronimoType; }
      set { m_TetronimoType = value; }
      }

    public GameTileColors Color
      {
      get
        {
        return m_Color;
        }
      }

    public Tetronimo( TetronimoType type )
      {
      m_Form = new Point[4];
      CreateTetronimo( type );
      }

    private void CreateTetronimo( TetronimoType type )
      {
      switch ( type )
        {
        case TetronimoType.I:
          CreateI();
          break;

        case TetronimoType.J:
          CreateJ();
          break;

        case TetronimoType.L:
          CreateL();
          break;

        case TetronimoType.O:
          CreateO();
          break;

        case TetronimoType.S:
          CreateS();
          break;

        case TetronimoType.T:
          CreateT();
          break;

        case TetronimoType.Z:
          CreateZ();
          break;

        case TetronimoType.Unknown:
          throw new TypeInitializationException( "Tetronimo", null );
        default:
          throw new TypeInitializationException( "Tetronimo", null );
        }
      }

    private void CreateI()
      {
      var index = 0;
      this.TetronimoType = TetrisCSharp.TetronimoType.I;
      for ( int i = 16; i < 77; i = i + 20 )
        {
        m_Form[index] = new Point( 36, i );
        index++;
        }
      m_Color = GameTileColors.Tuerkis;
      }

    private void CreateJ()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.J;
      m_Form[3] = new Point( 16, 76 );
      for ( int i = 36; i < 77; i = i + 20 )
        {
        m_Form[index] = new Point( 36, i );
        index++;
        }
      m_Color = GameTileColors.Blau;
      }

    private void CreateL()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.L;
      m_Form[3] = new Point( 56, 76 );
      for ( int i = 36; i < 77; i = i + 20 )
        {
        m_Form[index] = new Point( 36, i );
        index++;
        }
      m_Color = GameTileColors.Orange;
      }

    private void CreateO()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.O;
      for ( int i = 36; i < 57; i = i + 20 )
        {
        m_Form[index] = new Point( 20, i );
        m_Form[index + 2] = new Point( 40, i );
        index++;
        }
      m_Color = GameTileColors.Gelb;
      }

    private void CreateS()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.S;
      for ( int i = 36; i < 57; i = i + 20 )
        {
        m_Form[index] = new Point( i, 36 );
        m_Form[index + 2] = new Point( i - 20, 56 );
        index++;
        }
      m_Color = GameTileColors.Gruen;
      }

    private void CreateT()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.T;
      m_Form[3] = new Point( 36, 56 );
      for ( int i = 16; i < 57; i = i + 20 )
        {
        m_Form[index] = new Point( i, 36 );
        index++;
        }
      m_Color = GameTileColors.Lila;
      }

    private void CreateZ()
      {
      var index = 0;
      this.m_TetronimoType = TetrisCSharp.TetronimoType.Z;
      for ( int i = 16; i < 37; i = i + 20 )
        {
        m_Form[index] = new Point( i, 36 );
        m_Form[index + 2] = new Point( i + 20, 56 );
        index++;
        }
      m_Color = GameTileColors.Rot;
      }
    }
  }
