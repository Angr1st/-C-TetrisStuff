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
    public partial class ScoreboardForm : Form
    {
        private int m_Score;
        private ScoreBoardHtmlWorker m_ScoreboardHtmlWorker;
        private bool m_IsSubmit = false;
        public ScoreboardForm(bool isSubmit, int score) : this()
        {
            if (!isSubmit)
            {
                submitButton.Hide();
                nameBox.Hide();
                m_Score = score;
                m_IsSubmit = isSubmit;
            }
        }

        public ScoreboardForm()
        {
            InitializeComponent();
            m_ScoreboardHtmlWorker = new ScoreBoardHtmlWorker();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(nameBox.Text))
            {
                var ergebnis = m_ScoreboardHtmlWorker.SubmitScore(nameBox.Text, m_Score);
                Hide();
                if (ergebnis)
                {
                    MessageBox.Show("Dein Highscore wurde gesendet!");
                }
                else
                {
                    MessageBox.Show("Dein Highscore konnte nicht gesendet werden!");
                }
                Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ScoreboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
