using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisCSharp
  {
  public enum GameTileColors
    {
    Blau,
    Gelb,
    Gruen,
    Lila,
    Orange,
    Rot,
    Tuerkis,
    Schwarz
    }

  class GameTile
    {
    private GameTileColors m_Color;
    private Point m_GridPosition;
    private bool m_IsPositioned = false;
    private bool m_IsTetronimo = false;
    private bool m_HasReachedGround = false;
    private int m_ID = 0;//[01][01] = Arrayposition [0,0] ; [10][01] = Arrayposition[0,9]

    public static int GetColumnFromID( int id )//Vorne von Der ID repräsentiert hinten im Array (Spalte)
      {
      int result = id / 100;
      result = result - 1;
      return result;
      }

    public static int GetRowFromID( int id )//Hinterer Teil der ID repräsentiert vorne im Array (Reihe)
      {
      int result = id - ( id / 100 ) * 100;
      result = result - 1;
      return result;
      }

    public bool HasReachedGround
      {
      get { return m_HasReachedGround; }
      set
        {
        if ( m_HasReachedGround && !value )
          {
          IsTetronimo = false;
          }
        m_HasReachedGround = value;
        }
      }

    public bool IsTetronimo
      {
      get { return m_IsTetronimo; }
      set
        {
        if ( !value )
          {
          Color = GameTileColors.Schwarz;
          }

        m_IsTetronimo = value;
        }
      }

    public Point GridPosition
      {
      get { return m_GridPosition; }
      set
        {
        if ( !m_IsPositioned )
          {
          m_GridPosition = value;
          }
        }
      }

    public GameTileColors Color
      {
      get { return m_Color; }
      set
        {
        m_Color = value;
        if ( GameTileColor_Changed != null )
          {
          GameTileColor_Changed.Invoke( this, new GameTileColorChangedEventArgs( this ) );
          }
        }
      }

    public int ID
      {
      get
        {
        return m_ID;
        }
      }

    public GameTile( GameGrid creator, int id )
      {
      m_ID = id;
      m_Color = GameTileColors.Schwarz;
      GameTileColor_Changed += creator.CollectGameTileColorChanges;
      }

    public GameTile( Point gridPosition, GameGrid creator, int id )
      : this( creator, id )
      {
      GridPosition = gridPosition;
      }

    public event EventHandler<GameTileColorChangedEventArgs> GameTileColor_Changed;
    }
  }
