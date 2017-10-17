using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  class GameLogic
    {
    private int m_NumberOfClearedRows = 0;
    public event EventHandler<RowCompleteEventArgs> GameGrid_RowComplete;
    public event EventHandler GameLogic_CreateNewTetronimo;
    public event EventHandler GameLogic_PauseGame;
    public event EventHandler GameLogic_UnpauseGame;
    public event EventHandler GameLogic_GameLost;
    public event EventHandler<GameSpeedUpEventArgs> GameLogic_GameSpeedUp;

    public int NumberOfClearedRows
      {
      get
        {
        GameSpeedUp();
        return m_NumberOfClearedRows;
        }
      set
        {
        m_NumberOfClearedRows = m_NumberOfClearedRows + value;
        GameSpeedUp();
        }
      }

    private void GameSpeedUp()
      {
        GameLogic_GameSpeedUp.Invoke( this, new GameSpeedUpEventArgs(m_NumberOfClearedRows / 5 * -25 ) );
      }

    public GameLogic( GameController creator )
      {
      creator.GameState_Changed += ReactToChangedGameState;
      creator.GameController_GameTick_Happned += ProcessTick;
      GameLogic_CreateNewTetronimo += creator.CreateNewTetronimo;
      }

    public void GameLoop( GameStates gameState, GameGrid gameGrid )
      {
      ProcessGame( gameState, gameGrid );

      //UpdateUI();
      }

    private void UpdateUI()//Um die UI  dazu zu bringen sich nur neu zu zeichnen wenn ich das möchte wäre nur mit zugriff auf win32 api möglich deshalb wird davon abgesehen
      { }

    private void ProcessGame( GameStates gameState, GameGrid gameGrid )
      {
      if ( IsGameStateRunning( gameState ) )
        {
        if ( gameGrid.IsThereATetronimo() )
          {
          gameGrid.PrepareMoveTetronimoDown();
          }
        else if ( gameGrid.IsTetronimoGrounded )
          {
          var eventArgs = gameGrid.CheckForFullRow();
          if ( eventArgs != null )
            {
            NumberOfClearedRows = eventArgs.RowsComplete;
            gameGrid.CleanUpFullRow();
            GameGrid_RowComplete.Invoke( this, eventArgs );
            }
          }
        else
          {
          GameLogic_CreateNewTetronimo.Invoke( this, new EventArgs() );
          }
        }
      }

    private bool IsGameStateRunning( GameStates gameState )
      {
      if ( gameState == GameStates.Running )
        {
        return true;
        }
      return false;
      }

    public void ProcessTick( object sender, TickProcessEventArgs e )
      {
      GameLoop( e.GameState, e.GameGrid );
      }

    private void ReactToChangedGameState( object sender, GameStateEventArgs e )
      {
      switch ( e.GameState )
        {
        case GameStates.Running:
          Continue();
          break;
        case GameStates.Paused:
          PauseGame();
          break;
        case GameStates.Lost:
          InitiateGameLost();
          break;
        case GameStates.Pre:
          StartGame();
          break;
        case GameStates.Error:
          CrashGame();
          break;
        default:
          CrashGame();
          break;
        }
      }

    private void CrashGame()
      {
      throw new NotImplementedException();
      }

    private void StartGame()
      {
      throw new NotImplementedException();
      }

    private void InitiateGameLost()
      {
      GameLogic_GameLost.Invoke( this, new EventArgs() );
      }

    private void InitiateGameWonRoutine()
      {
      throw new NotImplementedException();
      }

    private void PauseGame()
      {
      //Pause Timer
      }

    private void Continue()
      {
      //Restart Timer
      }
    }
  }
