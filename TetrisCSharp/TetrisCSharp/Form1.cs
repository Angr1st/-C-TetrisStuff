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
    public partial class Form1 : Form
    {
        ScoreboardForm m_ScoreboardForm;
        GameForm m_GameForm;
        private System.Threading.Thread m_GameThread;
        private int m_Score = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            m_GameForm = new GameForm();
            this.Hide();
            m_GameForm.FormClosed += HandleGameFormClosed;
            m_GameThread = new System.Threading.Thread(() => Application.Run(m_GameForm));
            m_GameForm.Show();
            m_GameForm.StartGame();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HandleGameFormClosed(object sender, EventArgs e)
        {
            m_Score = m_GameForm.Score;
            m_GameForm.FormClosed -= HandleGameFormClosed;
            this.Show();
        }

        private void musicToggleButton_Click(object sender, EventArgs e)
        {

        }

        private void scoreBoardButton_Click(object sender, EventArgs e)
        {
            m_ScoreboardForm = new ScoreboardForm();
            this.Hide();

        }
    }
}
