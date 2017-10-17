using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  class GameController
    {
    private GameGrid m_GameGrid;
    private GameLogic m_GameLogic;
    private NextTetronimo m_NextTetronimo;
    private ScoreTracker m_ScoreTracker;
    private GameState m_GameState;
    private Tetronimo m_NewTetronimo;
    private Tetronimo m_OldTetronimo;
    public event EventHandler<GameStateEventArgs> GameState_Changed;
    public event EventHandler<TickProcessEventArgs> GameController_GameTick_Happned;
    public event EventHandler<NewTetronimoEventArgs> GameController_NewTetronimoCreated;

    public GameStates GameState
      {
      get { return m_GameState.GetGameState; }
      }

    internal void CreateNewTetronimo( object sender, EventArgs e )
      {
      m_OldTetronimo = m_NewTetronimo;
      m_NewTetronimo = m_NextTetronimo.GetNextTetronimo();
      GameController_NewTetronimoCreated.Invoke( this, new NewTetronimoEventArgs( m_NewTetronimo ) );
      m_GameGrid.DisplayNewTetronimo( m_OldTetronimo );
      }

    public GameController()
      {
      InitialiseGame();
      }

    public void InitialisePredictionBox( GameForm creator )
      {
      m_NewTetronimo = m_NextTetronimo.GetNextTetronimo();
      GameController_NewTetronimoCreated.Invoke( this, new NewTetronimoEventArgs( m_NewTetronimo ) );
      GameController_NewTetronimoCreated -= creator.SetUpPredictionBox;
      GameController_NewTetronimoCreated += creator.SyncUpPredictionBox;
      }

    public void InitialiseGrid()
      {
      m_GameGrid.Initialize();
      }

    public void StartGame()
      {
      if ( m_GameState.GetGameState == GameStates.Error )
        {
        GameState_Changed.Invoke( this, new GameStateEventArgs( GameStates.Error ) );
        }
      else if ( m_GameState.GetGameState != GameStates.Running && m_GameState.GetGameState != GameStates.Paused )
        {
        GameState_Changed.Invoke( this, new GameStateEventArgs( GameStates.Running ) );



        }
      }

    internal void HookUpEvents( GameForm creator )
      {
      m_GameGrid.GameGrid_Initialised += creator.SetUpGameGridRepresentation;
      m_GameGrid.GameGrid_Changed += creator.SyncRepresentationToGameGrid;
      m_GameGrid.GameGrid_TetronimoGrounded += creator.GroundTetronimo;
      creator.GameForm_UserMovedTetronimo += m_GameGrid.UserMovesTetronimo;
      creator.GameTick_Happned += GameForm_GameTick_Happned;
      m_ScoreTracker.Subscribe( m_GameLogic );
      GameController_NewTetronimoCreated += creator.SetUpPredictionBox;
      m_ScoreTracker.ScoreTracker_ScoreChangedEvent += creator.ReacToScoreChanged;
      m_GameLogic.GameLogic_PauseGame += creator.PauseGame;
      m_GameLogic.GameLogic_UnpauseGame += creator.UnPauseGame;
      m_GameGrid.GameGrid_GameLost += GameLost;
      m_GameLogic.GameLogic_GameLost += creator.ReactToGameLost;
      m_GameLogic.GameLogic_GameSpeedUp += creator.ReactToGameSpeedUp;
      //Weitere Events hier subscriben
      }

    private void GameForm_GameTick_Happned( object sender, EventArgs e )
      {
      GameController_GameTick_Happned.Invoke( this, new TickProcessEventArgs( m_GameGrid, m_GameState.GetGameState ) );
      }

    internal void UnsubscribeEvents( GameForm creator )
      {
      m_GameGrid.GameGrid_Initialised -= creator.SetUpGameGridRepresentation;
      m_GameGrid.GameGrid_Changed -= creator.SyncRepresentationToGameGrid;
      m_GameGrid.GameGrid_TetronimoGrounded -= creator.GroundTetronimo;
      creator.GameForm_UserMovedTetronimo -= m_GameGrid.UserMovesTetronimo;
      creator.GameTick_Happned -= GameForm_GameTick_Happned;
      m_ScoreTracker.Unsubscribe( m_GameLogic );
      GameController_NewTetronimoCreated -= creator.SetUpPredictionBox;
      m_ScoreTracker.ScoreTracker_ScoreChangedEvent -= creator.ReacToScoreChanged;
      m_GameLogic.GameLogic_PauseGame -= creator.PauseGame;
      m_GameLogic.GameLogic_UnpauseGame -= creator.UnPauseGame;
      m_GameGrid.GameGrid_GameLost -= GameLost;
      m_GameLogic.GameLogic_GameLost -= creator.ReactToGameLost;
      m_GameLogic.GameLogic_GameSpeedUp -= creator.ReactToGameSpeedUp;
      }

    public void GameLost( object sender, GameStateEventArgs e )
      {
      GameState_Changed.Invoke( this, e );
      }

    public void PauseGame()
      {
      if ( m_GameState.GetGameState == GameStates.Paused )
        {
        UnpauseGame();
        }
      else
        {
        GameState_Changed.Invoke( this, new GameStateEventArgs( GameStates.Paused ) );
        }
      }

    private void RestartGame()
      {
      m_GameState.Unsubscribe( this );
      InitialiseGame();
      }

    private void InitialiseGame()
      {
      m_GameGrid = new GameGrid();
      m_GameLogic = new GameLogic( this );
      m_NextTetronimo = new NextTetronimo();
      m_ScoreTracker = new ScoreTracker();
      m_GameState = new GameState( this );
      }

    private void UnpauseGame()
      {
      GameState_Changed.Invoke( this, new GameStateEventArgs( GameStates.Running ) );
      }

    }
  }
