using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  public enum GameStates
    {
    Running,
    Paused,
    Lost,
    Pre,
    Error
    }

  class GameState
    {
    private GameStates m_GameState;
    
    public GameStates GetGameState
      {
      get { return m_GameState; }
      }

    public GameState( GameController creator)
      {
      m_GameState = GameStates.Pre;
      creator.GameState_Changed += OnGameState_Changed;
      }

    internal void Unsubscribe(GameController creator)
      {
      creator.GameState_Changed -= OnGameState_Changed;
      }

    private void OnGameState_Changed(object sender, GameStateEventArgs e)
      {
      if ( e.GameState != m_GameState )
        {
        m_GameState = e.GameState;
        }
      }
    }
  }
