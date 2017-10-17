using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisCSharp
  {
  public partial class GameForm : Form
    {
    private int m_BaseTick = 750;
    private int m_MaxTick = 25;
    private int m_LastTick = 750;
    private int m_Score = 0;
    private List<GameTile> m_CurrentTetronimo;
    private GameController m_GameController;
    private Timer m_GameTicks;
    private TetronimoConverter m_TetronimoConverter;
    public event EventHandler GameTick_Happned;
    public event EventHandler GameForm_Initialise;
    internal event EventHandler<TetronimoMovementEventArgs> GameForm_UserMovedTetronimo;

    public int Score { get; }

    public GameForm()
      {
      m_CurrentTetronimo = new List<GameTile>( 4 );
      m_TetronimoConverter = new TetronimoConverter();
      m_GameController = new GameController();
      m_GameTicks = new Timer();
      m_GameTicks.Interval = m_LastTick; // One Second Intervall
      InitializeComponent();
      m_GameController.HookUpEvents( this );
      m_GameTicks.Tick += GameTicks_Tick;
      GameForm_Initialise += GameForm_GameForm_Initialise;
      }

    internal void StartGame()
      {
      GameForm_Initialise.Invoke( this, new EventArgs() );
      }

    private void GameForm_GameForm_Initialise( object sender, EventArgs e )
      {
      m_GameController.InitialisePredictionBox( this );
      m_GameController.InitialiseGrid();

      m_GameTicks.Enabled = true;
      m_GameController.StartGame();
      }

    private void GameTicks_Tick( object sender, EventArgs e )
      {
      m_GameTicks.Enabled = true;
      GameTick_Happned.Invoke( this, new EventArgs() );
      }

    internal void SyncRepresentationToGameGrid( object sender, GameTileColorChangedEventArgs e )
      {
      var foundControls = gameBox.Controls.Find( e.NewGameTile.ID.ToString(), false );
      foreach ( PictureBox pictureControl in foundControls )
        {
        pictureControl.Image = TetronimoConverter.SelectBlockImage( e.NewGameTile.Color );
        pictureControl.Invalidate();
        }
      }

    internal void SetUpGameGridRepresentation( object sender, GameGridEventArgs e )
      {
      foreach ( var item in e.GameGrid )
        {
        var hinzuzufügendePictureBox = new PictureBox();
        hinzuzufügendePictureBox.Size = new Size( 20, 20 );
        hinzuzufügendePictureBox.Location = item.GridPosition;
        hinzuzufügendePictureBox.BackColor = Color.Black;
        hinzuzufügendePictureBox.Name = item.ID.ToString();
        gameBox.Controls.Add( hinzuzufügendePictureBox );
        }

      foreach ( PictureBox item in gameBox.Controls )
        {
        item.Show();
        }

      }

    internal void GroundTetronimo( object sender, EventArgs e )
      {
      m_CurrentTetronimo = new List<GameTile>( 4 );
      }

    internal void SyncUpPredictionBox( object sender, NewTetronimoEventArgs e )
      {
      List<PictureBox> newTetronimo = m_TetronimoConverter.ConvertToPictureBox( e.NewTetronimo );
      for ( int i = 0; i < 4; i++ )
        {
        PictureBox pictureBox = (PictureBox)predictionBox.Controls[i];
        pictureBox.Location = newTetronimo[i].Location;
        pictureBox.Image = newTetronimo[i].Image;
        }
      }

    internal void SetUpPredictionBox( object sender, NewTetronimoEventArgs e )
      {
      foreach ( var item in m_TetronimoConverter.ConvertToPictureBox( e.NewTetronimo ) )
        {
        predictionBox.Controls.Add( item );
        }

      foreach ( PictureBox item in predictionBox.Controls )
        {
        item.Show();
        }
      }

    internal void PauseGame( object sender, EventArgs e )
      {
      m_GameTicks.Enabled = false;
      }

    internal void UnPauseGame( object sender, EventArgs e )
      {
      m_GameTicks.Enabled = true;
      }

    internal void ReacToScoreChanged( object sender, ScoreChangedEventArgs e )
      {
      m_Score = e.Score;
      scoreBox.Controls[0].Text = e.Score.ToString();
      }

    internal void ReactToGameSpeedUp( object sender, GameSpeedUpEventArgs e )
      {
      m_LastTick = m_BaseTick + e.SpeedUp;
      ChangeTickRate(m_BaseTick + e.SpeedUp );
      }

    private void ChangeTickRate( int milisec )
      {
      if ( milisec >= m_MaxTick )
        {
        m_GameTicks.Interval = milisec;
        }

      }

    private void GameForm_FormClosed( object sender, EventArgs e )
      {

      }

    public void ReactToGameLost( object sender, EventArgs e )
      {
      EnsureGamePaused();
      MessageBox.Show( String.Format( "Score reached : {0}!", scoreBox.Controls[0].Text ) );
      Close();
      }

    private void GameForm_KeyPress( object sender, KeyPressEventArgs e )
      {
      if ( e.KeyChar == (char)112 )//p Pausiert das Spiel
        {
        m_GameController.PauseGame();
        e.Handled = true;
        }
      if ( m_GameController.GameState == GameStates.Running && ( e.KeyChar == 97 || e.KeyChar == 115 || e.KeyChar == 100 || e.KeyChar == 106 || e.KeyChar == 108 || e.KeyChar == 112 ) )
        {
        switch ( e.KeyChar )
          {
          //case (char)115://s Lässt den Stein schneller fallen
          //  var tickRate = m_GameTicks.Interval;
          //  ChangeTickRate( m_MaxTick );
          //  e.Handled = true;
          //  break;
          case (char)97://a Move Left
            GameForm_UserMovedTetronimo.Invoke( this, new TetronimoMovementEventArgs( TetronimoMovement.Left ) );
            e.Handled = true;
            break;
          case (char)100://d Move Right
            GameForm_UserMovedTetronimo.Invoke( this, new TetronimoMovementEventArgs( TetronimoMovement.Right ) );
            e.Handled = true;
            break;
          case (char)106://j Roll Left
            GameForm_UserMovedTetronimo.Invoke( this, new TetronimoMovementEventArgs( TetronimoMovement.RollLeft ) );
            e.Handled = true;
            break;
          case (char)108://i Roll Right
            GameForm_UserMovedTetronimo.Invoke( this, new TetronimoMovementEventArgs( TetronimoMovement.RollRight ) );
            e.Handled = true;
            break;
          }
        }
      }

    private void GameForm_FormClosing( object sender, FormClosingEventArgs e )
      {
      EnsureGamePaused();
      }

    private void EnsureGamePaused()
      {
      if ( m_GameController.GameState == GameStates.Running )
        {
        m_GameController.PauseGame();
        }
      }

    private void GameForm_KeyDown( object sender, KeyEventArgs e )
      {
      if ( e.KeyData == Keys.S )
        {
        ChangeTickRate( m_MaxTick );
        }
      }

    private void GameForm_KeyUp( object sender, KeyEventArgs e )
      {
      if ( e.KeyData == Keys.S )
        {
        ChangeTickRate( m_LastTick );
        }
      }
    }
  }
