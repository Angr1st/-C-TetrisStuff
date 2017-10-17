using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCSharp
  {
  class ScoreTracker
    {
    public event EventHandler<ScoreChangedEventArgs> ScoreTracker_ScoreChangedEvent;
    private int m_Score = 0;
    public int Score { get; private set; } = 0;

    public void Subscribe(GameLogic sender)
      {
      sender.GameGrid_RowComplete += RowComplete;
      }

    public void Unsubscribe( GameLogic sender )
      {
      sender.GameGrid_RowComplete -= RowComplete;
      }

    private void RowComplete(object sender, RowCompleteEventArgs e)
      {
      m_Score = m_Score + 100 * e.RowsComplete* e.RowsComplete;
      Score = m_Score;
      ScoreTracker_ScoreChangedEvent.Invoke( this, new ScoreChangedEventArgs( m_Score ) );
      }
    }
  }
