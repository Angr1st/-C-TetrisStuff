using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  internal partial class GameGrid
    {
    private bool m_HasRotationStateChanged;

    private void RotateTetronimo( TetronimoMovement move )
      {
      if ( m_Tetronimo != null && m_Tetronimo.Count == 4 )
        {
        switch ( m_TetronimoType )
          {
          case TetronimoType.I:
            RotateI( move );
            break;
          case TetronimoType.J:
            RotateJ( move );
            break;
          case TetronimoType.L:
            RotateL( move );
            break;
          case TetronimoType.O:
            break;
          case TetronimoType.S:
            RotateS( move );
            break;
          case TetronimoType.T:
            RotateT( move );
            break;
          case TetronimoType.Z:
            RotateZ( move );
            break;
          case TetronimoType.Unknown:
            break;
          default:
            break;
          }
        }
      }

    private TetronimoRotationState NextState( TetronimoMovement move )
      {
      if ( move == TetronimoMovement.RollRight )
        {
        switch ( m_RotationState )
          {
          case TetronimoRotationState.Eins:
            return TetronimoRotationState.Vier;

          case TetronimoRotationState.Zwei:
            return TetronimoRotationState.Eins;

          case TetronimoRotationState.Drei:
            return TetronimoRotationState.Zwei;

          case TetronimoRotationState.Vier:
            return TetronimoRotationState.Drei;

          default:
            return TetronimoRotationState.Error;
          }
        }
      else if ( move == TetronimoMovement.RollLeft )
        {
        switch ( m_RotationState )
          {
          case TetronimoRotationState.Eins:
            return TetronimoRotationState.Zwei;

          case TetronimoRotationState.Zwei:
            return TetronimoRotationState.Drei;

          case TetronimoRotationState.Drei:
            return TetronimoRotationState.Vier;

          case TetronimoRotationState.Vier:
            return TetronimoRotationState.Eins;

          default:
            return TetronimoRotationState.Error;
          }
        }
      return TetronimoRotationState.Error;
      }

    private bool IsViabelID( int id )
      {

      if ( id < 101 && id < 3410 )//lowest ID is 101 für die linke obere Ecke und highest ist 3410 für untere rechte
        {
        return false;
        }
      else if ( IsViableColumn( id ) && IsViableRow( id ) )
        {
        return true;
        }
      else
        {
        return false;
        }
      }

    private bool IsViableColumn( int id )
      {
      var zuPruefendeZahl = GameTile.GetColumnFromID( id );//Null-Indieziert
      if ( zuPruefendeZahl <= -1 || zuPruefendeZahl > 33 )
        {
        return false;
        }
      else
        {
        return true;
        }
      }

    private bool IsViableRow( int id )
      {
      var zuPruefendeZahl = GameTile.GetRowFromID( id );//Null-Indieziert
      if ( zuPruefendeZahl <= -1 || zuPruefendeZahl > 9 )
        {
        return false;
        }
      else
        {
        return true;
        }
      }


    private bool IsPartOfTetronimoInGround(int id)
      {
      if ( GameTile.GetColumnFromID( id ) + 1 == 31 )
        {
        return true;
        }
      return false;
      }

    private List<GameTile> GetNextTetronimoRotationalPosition( int[] newPositionAsID )
      {
      List<GameTile> resultList = new List<GameTile>();
      foreach ( var item in newPositionAsID )
        {
        if ( IsViabelID( item ) )
          {
          var newBlock = FindGameTileByID( item );
          if ( !newBlock.HasReachedGround && !IsPartOfTetronimoInGround( newBlock.ID ) )
            {
            resultList.Add( newBlock );
            }
          }
        else//Eine der Positionen gibt es nicht
          {
          return null;
          }
        }

      return resultList;
      }

    private void OverrideBlocks( int[] blocksThatShallBeOverwrittenIDs )
      {
      OverrideBlocks( GetNextTetronimoRotationalPosition( blocksThatShallBeOverwrittenIDs ) );
      }

    private void OverrideBlocks( List<GameTile> blocksThatShallBeOverwritten )
      {
      if ( blocksThatShallBeOverwritten != null && blocksThatShallBeOverwritten.Count == 4 )
        {
        List<GameTile> newTetronimo, copyOfCurrentTetronimo;
        CopyOldTetronimoAndDelete( out newTetronimo, out copyOfCurrentTetronimo );
        foreach ( var item in blocksThatShallBeOverwritten )
          {
          item.Color = copyOfCurrentTetronimo[0].Color;
          item.IsTetronimo = true;
          newTetronimo.Add( item );
          }
        m_Tetronimo = newTetronimo;
        m_HasRotationStateChanged = true;
        }
      }

    private void Rotate( TetronimoRotationState nextRotationState, int[] blockIDs )
      {
      OverrideBlocks( blockIDs );
      if ( m_HasRotationStateChanged )
        {
        m_RotationState = nextRotationState;
        m_HasRotationStateChanged = false;
        }
      }

    private int GetIDRelativFromOldID( int oldId, int zeile, int spalte )
      {
      int oldZeile = GameTile.GetColumnFromID( oldId );
      int oldSpalte = GameTile.GetRowFromID( oldId );
      int newZeile = oldZeile + zeile;
      if ( newZeile > 33 )
        {
        newZeile = newZeile - 34;
        }
      else if ( newZeile < 0 )
        {
        newZeile = newZeile + 34;
        }
      var newSpalte = oldSpalte + spalte;
      if ( newSpalte < 0 || newSpalte > 9 )
        {
        return -1;
        }
      return CreateID( newSpalte, newZeile );
      }

    private void RotateS(TetronimoMovement move)
      {
      var nextRotationState = NextState( move );           
      int[] blockIDs = new int[4];                         // 12
      if ( m_RotationState == TetronimoRotationState.Eins )//34 
        {                                                     
        if ( nextRotationState == TetronimoRotationState.Vier )
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -2, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, -1 );
          }
        else//rotationstate.Zwei 
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, +2 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, -2 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -1 );
          }
        else//TetronimoRotationstate.Drei  
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -2, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, -1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +2, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, +1 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, -2 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, +2 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +1 );
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = m_Tetronimo[0].ID;
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +2, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, +1 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }

    private void RotateZ(TetronimoMovement move)
      {
      var nextRotationState = NextState( move );           
      int[] blockIDs = new int[4];                         //12
      if ( m_RotationState == TetronimoRotationState.Eins )// 34
        {                                                   
        if ( nextRotationState == TetronimoRotationState.Vier )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }
        else//rotationstate.Zwei 
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1,-1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }
        else//TetronimoRotationstate.Drei  
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }

    private void RotateI(TetronimoMovement move)
      {
      var nextRotationState = NextState( move );           // 1
      int[] blockIDs = new int[4];                         // 2
      if ( m_RotationState == TetronimoRotationState.Eins )// 3
        {                                                   //4    
        if ( nextRotationState == TetronimoRotationState.Vier )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +2 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, 0, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, -1 );
          }
        else//rotationstate.Zwei 
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +2, -1 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, 0 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -2, +1 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, 0 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -2 );
          }
        else//TetronimoRotationstate.Drei  
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +2 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, 0, +1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, -1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -2 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, 0, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, +1 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -2, +1 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, -1, 0 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +2, -1 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, +1, 0 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, 0, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +2 );
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -2 );
          blockIDs[1] = GetIDRelativFromOldID( m_Tetronimo[1].ID, 0, -1 );
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, 0 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, +1 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }



    private void RotateJ(TetronimoMovement move)
      {
      var nextRotationState = NextState( move );           // 1
      int[] blockIDs = new int[4];                         // 2
      if ( m_RotationState == TetronimoRotationState.Eins )//43
        {                                                      //4 
        if ( nextRotationState == TetronimoRotationState.Vier )//321
          {                                                    //
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );
          }                   //   
        else//rotationstate.Zwei 123
          {                 //     4
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }                             // 34
        else//TetronimoRotationstate.Drei  2
          {                            //  1
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2);
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }

    private void RotateL(TetronimoMovement move)
      {
      var nextRotationState = NextState( move );           //1
      int[] blockIDs = new int[4];                         //2
      if ( m_RotationState == TetronimoRotationState.Eins )//34
        {                                                      // 
        if ( nextRotationState == TetronimoRotationState.Vier )//321
          {                                                    //4
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }                   //   4
        else//rotationstate.Zwei 123
          {                 //    
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );//eine Reihe runter und nach Rechts
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );//eine reihe hoch und ein nach links
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );//eine Reihe nach oben und ein nach rechts
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }                             //43
        else//TetronimoRotationstate.Drei  2
          {                            //  1
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, -2 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +2, 0 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -2, 0 );
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, 0, +2 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }

    private void RotateT( TetronimoMovement move )
      {
      var nextRotationState = NextState( move );
      int[] blockIDs = new int[4];                         //123
      if ( m_RotationState == TetronimoRotationState.Eins )// 4
        {                                                      // 1
        if ( nextRotationState == TetronimoRotationState.Vier )//42
          {                                                    // 3
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );//eine Reihe nach oben und ein nach rechts
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );//eine Reihe nach Unten und ein nach Links
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, -1 );//eine reihe hoch und ein nach links
          }                   //  3
        else//rotationstate.Zwei  24
          {                 //    1
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );//eine Reihe runter und nach Rechts
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );//eine reihe hoch und ein nach links
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +1 );//eine Reihe nach oben und ein nach rechts
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Zwei )
        {
        if ( nextRotationState == TetronimoRotationState.Eins )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -1 );
          }                             // 4
        else//TetronimoRotationstate.Drei 321
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, -1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Drei )
        {
        if ( nextRotationState == TetronimoRotationState.Zwei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, +1 );
          }
        else//Rotationstate.Vier
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, -1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, +1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, -1 );
          }
        }
      else if ( m_RotationState == TetronimoRotationState.Vier )
        {
        if ( nextRotationState == TetronimoRotationState.Drei )
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, +1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, -1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, -1, +1 );
          }
        else//Rotationstate.eins
          {
          blockIDs[0] = GetIDRelativFromOldID( m_Tetronimo[0].ID, +1, -1 );
          blockIDs[1] = m_Tetronimo[1].ID;
          blockIDs[2] = GetIDRelativFromOldID( m_Tetronimo[2].ID, -1, +1 );
          blockIDs[3] = GetIDRelativFromOldID( m_Tetronimo[3].ID, +1, +1 );
          }
        }
      else
        {
        throw new ArgumentException();
        }
      Rotate( nextRotationState, blockIDs );
      }
    }
  }
