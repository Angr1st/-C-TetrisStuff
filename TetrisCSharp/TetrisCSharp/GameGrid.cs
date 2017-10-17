using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  public enum TetronimoMovement
    {
    Left,
    Right,
    RollLeft,
    RollRight
    }

  public enum BlockMovement
    {
    Down,
    Left,
    Right
    }

  public enum TetronimoRotationState//Im Uhrzeigersinn von den Startpositionen aus; Jede Drehung ändert den State um eins
    {
    Eins,
    Zwei,
    Drei,
    Vier,
    Error
    }

  internal partial class GameGrid
    {
    private TetronimoRotationState m_RotationState = TetronimoRotationState.Eins;
    private TetronimoType m_TetronimoType = TetronimoType.Unknown;
    private TetronimoConverter m_TConverter;
    private GameTile[,] m_GameGrid = null;
    private bool m_IsTetronimoGrounded = false;
    private List<GameTile> m_GameTileWaitList = new List<GameTile>( 8 );
    private List<GameTile> m_Tetronimo = new List<GameTile>( 4 );
    private List<int> m_FullRowList = null;
    public event EventHandler<GameTileColorChangedEventArgs> GameGrid_Changed;
    public event EventHandler<GameGridEventArgs> GameGrid_Initialised;
    public event EventHandler GameGrid_TetronimoGrounded;
    public event EventHandler<GameStateEventArgs> GameGrid_GameLost;
    public GameGrid()
      {
      m_GameGrid = new GameTile[10, 34];// Letzten Vier Reihen befinden sich über dem SpielFeld -> für Blockspawning
      m_TConverter = new TetronimoConverter();
      }

    public List<GameTile> Tetronimo { get { return new List<GameTile>( m_Tetronimo ); } }

    public bool IsTetronimoGrounded { get { return m_IsTetronimoGrounded; } }

    public void UserMovesTetronimo( object sender, TetronimoMovementEventArgs e )
      {
      if ( m_Tetronimo != null )//Da es sonst zu crashes kommen kann da es null ist weil schon auf dem Boden angekommen
        {
        switch ( e.TetronimoMove )
          {
          case TetronimoMovement.Left:
          case TetronimoMovement.Right:
            MoveTetronimoLeftRight( e.TetronimoMove );
            break;
          case TetronimoMovement.RollLeft:
          case TetronimoMovement.RollRight:
            RotateTetronimo( e.TetronimoMove );
            break;
          default:
            break;
          }
        }
      }

    private void MoveTetronimoLeftRight( TetronimoMovement leftOrRight )
      {
      if ( leftOrRight != TetronimoMovement.Left && leftOrRight != TetronimoMovement.Right )
        {
        throw new ArgumentException( "Diese Methode bewegt den Tetronimo nur nach Links und Rechts andere TetronimoMovements sind unzulässig als Input" );
        }


      var unmoveable = false;
      foreach ( var item in m_Tetronimo )
        {
        if ( CheckForObstruction( item.ID, leftOrRight ) )
          {
          unmoveable = true;
          }
        }

      if ( !unmoveable )
        {
        MoveTetronimo( leftOrRight );
        }
      }

    private void MoveTetronimo( TetronimoMovement leftOrRight )
      {
      switch ( leftOrRight )
        {
        case TetronimoMovement.Left:
          MoveTetronimo( BlockMovement.Left );
          break;
        case TetronimoMovement.Right:
          MoveTetronimo( BlockMovement.Right );
          break;
        default:
          throw new ArgumentException();
        }
      }

    private bool CheckForObstruction( int id, TetronimoMovement move )
      {
      if ( CheckForLeftAndRightBounds( id, move ) )
        {
        return true;
        }
      GameTile neighboringGameTile;
      if ( move == TetronimoMovement.Left )
        {
        neighboringGameTile = FindGameTileByID( id - 1 );
        }
      else if ( move == TetronimoMovement.Right )
        {
        neighboringGameTile = FindGameTileByID( id + 1 );
        }
      else
        {
        throw new ArgumentException();
        }

      if ( neighboringGameTile.HasReachedGround )
        {
        return true;
        }
      else
        {
        return false;
        }
      }

    private bool CheckForLeftAndRightBounds( int id, TetronimoMovement move )
      {
      var row = GameTile.GetRowFromID( id );
      if ( move == TetronimoMovement.Left && row == -0 )
        {
        return true;
        }
      else if ( move == TetronimoMovement.Right && row == 9 )
        {
        return true;
        }
      else
        {
        return false;
        }
      }

    public void DisplayNewTetronimo( Tetronimo newBlock )
      {
      m_IsTetronimoGrounded = false;
      m_TetronimoType = newBlock.TetronimoType;
      m_RotationState = TetronimoRotationState.Eins;
      m_Tetronimo = new List<GameTile>( 4 );
      var idList = m_TConverter.ConvertToGameGrid( newBlock );
      foreach ( var item in idList )
        {
        var gameTile = FindGameTileByID( item );
        gameTile.Color = newBlock.Color;
        gameTile.IsTetronimo = true;
        m_Tetronimo.Add( gameTile );
        }
      }

    public void PrepareMoveTetronimoDown()
      {
      if ( IsTetronimoTouchingGround() )
        {
        GroundTetronimo();
        }
      else if ( HasEveryTetronimoFreeSpaceOrTetronimo() )
        {
        MoveTetronimo( BlockMovement.Down );
        }
      else
        {
        GroundTetronimo();
        }
      }

    internal void CleanUpFullRow()
      {
      foreach ( var item in m_FullRowList )
        {
        CleanUpRow( item );
        }
      m_FullRowList = null;
      }

    private void CleanUpRow( int rowIndex )
      {
      for ( int i = 0; i < 10; i++ )
        {
        m_GameGrid[i, rowIndex].HasReachedGround = false;
        }
      MoveRowDown( rowIndex );
      }

    private void MoveRowDown(int rowIndex)
      {
      for ( int j = rowIndex-1; j > -1 ; j-- )
        {
        for ( int i = 0; i < m_GameGrid.GetLength(0); i++ )
          {
          m_GameGrid[i, j + 1].Color = m_GameGrid[i, j].Color;
          m_GameGrid[i, j + 1].IsTetronimo = m_GameGrid[i, j].IsTetronimo;
          m_GameGrid[i, j + 1].HasReachedGround = m_GameGrid[i, j].HasReachedGround;
          m_GameGrid[i, j].HasReachedGround = false;
          }
        }
      }

    public RowCompleteEventArgs CheckForFullRow()
      {
      m_IsTetronimoGrounded = false;
      m_FullRowList = new List<int>();
      var completeRow = 0;
      var block = 0;
      for ( int j = 0; j < m_GameGrid.GetLength( 1 ); j++ )
        {
        block = 0;
        for ( int i = 0; i < m_GameGrid.GetLength( 0 ); i++ )
          {
          if ( m_GameGrid[i, j].IsTetronimo )
            {
            block++;
            }
          }
        if ( block == 10 )
          {
          completeRow++;
          m_FullRowList.Add( j );
          }
        }
      if ( completeRow == 0 )
        {
        return null;
        }
      return new RowCompleteEventArgs( completeRow );
      }

    private void MoveTetronimo( BlockMovement moveDirection )
      {
      List<GameTile> newTetronimo, copyOfCurrentTetronimo;
      CopyOldTetronimoAndDelete( out newTetronimo, out copyOfCurrentTetronimo );
      foreach ( var item in copyOfCurrentTetronimo )
        {
        OverrideBlock( moveDirection, newTetronimo, item );
        }
      m_Tetronimo = newTetronimo;
      }

    private void CopyOldTetronimoAndDelete( out List<GameTile> newTetronimo, out List<GameTile> copyOfCurrentTetronimo )
      {
      newTetronimo = new List<GameTile>( 4 );
      var color = m_Tetronimo[1].Color;
      copyOfCurrentTetronimo = CopyGameTileList( m_Tetronimo );
      foreach ( var item in m_Tetronimo )
        {
        item.IsTetronimo = false;
        }
      }

    private void OverrideBlock( BlockMovement moveDirection, List<GameTile> newTetronimo, GameTile item )
      {
      if ( moveDirection == BlockMovement.Down )
        {
        OverrideBlock( newTetronimo, item, FindGameTileByID( FindBlockBelow( item.ID ) ) );
        }
      else
        {
        OverrideBlock( newTetronimo, item, FindGameTileByID( FindBlock( item.ID, moveDirection ) ) );
        }
      }

    private void OverrideBlock( List<GameTile> newTetronimo, GameTile item, GameTile newBlock )
      {
      newBlock.Color = item.Color;
      newBlock.IsTetronimo = true;
      newTetronimo.Add( newBlock );
      }

    private List<GameTile> CopyGameTileList( List<GameTile> tetronimo )
      {
      List<GameTile> result = new List<GameTile>();
      foreach ( var item in tetronimo )
        {
        GameTile newGameTile = new GameTile( this, item.ID );
        newGameTile.Color = item.Color;
        newGameTile.IsTetronimo = true;
        result.Add( newGameTile );
        }
      return result;
      }

    private int FindBlock( int id, BlockMovement move )
      {
      var resultID = 0;
      switch ( move )
        {
        case BlockMovement.Down:
          resultID = FindBlockBelow( id );
          break;
        case BlockMovement.Left:
          resultID = id - 1;
          break;
        case BlockMovement.Right:
          resultID = id + 1;
          break;
        default:
          break;
        }
      return resultID;
      }

    private int FindBlockBelow( int id )
      {
      if ( GameTile.GetColumnFromID( id ) == 33 )
        {
        return GameTile.GetRowFromID( id ) + 101;
        }
      else
        {
        return id + 100;
        }
      }

    private bool isPartOfTetronimoInSpawningZoneGrounded()
      {
      foreach ( var item in m_Tetronimo )
        {
        if ( item.ID > 3101 && item.HasReachedGround )
          {
          return true;
          }
        }
      return false;
      }

    private bool IsTetronimoTouchingGround( List<GameTile> tetronimo )
      {
      var result = false;
      foreach ( var item in tetronimo )
        {
        if ( IsPartOFTetronimoTouchingGround( item.ID ) )
          {
          result = true;
          }
        }
      return result;
      }

    private bool IsTetronimoTouchingGround()
      {
      return IsTetronimoTouchingGround( m_Tetronimo );
      }

    private bool IsPartOFTetronimoTouchingGround( int id )
      {
      if ( GameTile.GetColumnFromID( id ) + 1 == 30 )
        {
        return true;
        }
      return false;
      }

    private void GroundTetronimo()
      {
      foreach ( var item in m_Tetronimo )
        {
        item.HasReachedGround = true;
        }
      if ( isPartOfTetronimoInSpawningZoneGrounded() )
        {
        GameGrid_GameLost.Invoke( this, new GameStateEventArgs( GameStates.Lost ) );
        }
      m_Tetronimo = null;
      m_IsTetronimoGrounded = true;
      GameGrid_TetronimoGrounded.Invoke( this, new EventArgs() );
      }

    private bool HasEveryTetronimoFreeSpaceOrTetronimo()
      {
      foreach ( var item in m_Tetronimo )
        {
        var blockBelow = FindGameTileByID( FindBlockBelow( item.ID ) );
        if ( blockBelow.IsTetronimo && blockBelow.HasReachedGround )
          {
          return false;
          }
        }
      return true;
      }

    private GameTile FindGameTileByID( int id )
      {
      return m_GameGrid[GameTile.GetRowFromID( id ), GameTile.GetColumnFromID( id )];
      }


    public bool IsThereATetronimo()
      {
      if ( m_Tetronimo != null && m_Tetronimo.Count != 0 )
        {
        return true;
        }
      else
        {
        return false;
        }
      }

    public bool IsTetronimoTouchingTheGround()
      {
      if ( !IsThereATetronimo() )
        {
        return false;
        }
      else
        {
        foreach ( var item in m_Tetronimo )
          {
          if ( item.HasReachedGround )
            {
            return true;
            }
          }
        }
      return false;
      }

    public void CollectGameTileColorChanges( object sender, GameTileColorChangedEventArgs e )
      {
      GameGrid_Changed.Invoke( this, e );
      }

    //private void SenderZurWaitListHinzufügen( object sender )
    //  {
    //  m_GameTileWaitList.Add( (GameTile)sender );
    //  m_GameTileColorChanges++;
    //  }

    private System.Drawing.Point CreatePoint( int i, int j )
      {
      if ( j == 30 )
        {
        return new System.Drawing.Point( i * 20 + 2, -70 );
        }
      else if ( j == 31 )
        {
        return new System.Drawing.Point( i * 20 + 2, -50 );
        }
      else if ( j == 32 )
        {
        return new System.Drawing.Point( i * 20 + 2, -30 );
        }
      else if ( j == 33 )
        {
        return new System.Drawing.Point( i * 20 + 2, -10 );
        }
      else
        {
        return new System.Drawing.Point( i * 20 + 2, j * 20 + 10 );
        }
      }

    private int CreateID( int i, int j )
      {
      var firstpart = j * 100 + 100;
      var secondpart = i + 1;
      return firstpart + secondpart;
      }

    public void Initialize()
      {
      for ( int i = 0; i < m_GameGrid.GetLength( 0 ); i++ )
        {
        for ( int j = 0; j < m_GameGrid.GetLength( 1 ); j++ )
          {
          m_GameGrid[i, j] = new GameTile( CreatePoint( i, j ), this, CreateID( i, j ) );
          }
        }
      GameGrid_Initialised.Invoke( this, new GameGridEventArgs( m_GameGrid, m_GameTileWaitList ) );
      }
    }
  }
