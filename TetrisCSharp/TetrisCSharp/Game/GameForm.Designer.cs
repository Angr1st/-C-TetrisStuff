namespace TetrisCSharp
  {
  partial class GameForm
    {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
      {
      if ( disposing && ( components != null ) )
        {
        components.Dispose();
        }
      base.Dispose( disposing );
      }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
      {
      this.predictionBox = new System.Windows.Forms.GroupBox();
      this.scoreBox = new System.Windows.Forms.GroupBox();
      this.ScoreDisplay = new System.Windows.Forms.Label();
      this.gameBox = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.scoreBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // predictionBox
      // 
      this.predictionBox.Location = new System.Drawing.Point(13, 13);
      this.predictionBox.Name = "predictionBox";
      this.predictionBox.Size = new System.Drawing.Size(92, 100);
      this.predictionBox.TabIndex = 0;
      this.predictionBox.TabStop = false;
      this.predictionBox.Text = "Next Block";
      // 
      // scoreBox
      // 
      this.scoreBox.Controls.Add(this.ScoreDisplay);
      this.scoreBox.Location = new System.Drawing.Point(13, 120);
      this.scoreBox.Name = "scoreBox";
      this.scoreBox.Size = new System.Drawing.Size(92, 100);
      this.scoreBox.TabIndex = 1;
      this.scoreBox.TabStop = false;
      this.scoreBox.Text = "Score";
      // 
      // ScoreDisplay
      // 
      this.ScoreDisplay.AutoSize = true;
      this.ScoreDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScoreDisplay.Location = new System.Drawing.Point(36, 46);
      this.ScoreDisplay.Name = "ScoreDisplay";
      this.ScoreDisplay.Size = new System.Drawing.Size(17, 18);
      this.ScoreDisplay.TabIndex = 0;
      this.ScoreDisplay.Text = "0";
      this.ScoreDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // gameBox
      // 
      this.gameBox.Location = new System.Drawing.Point(112, 13);
      this.gameBox.Name = "gameBox";
      this.gameBox.Size = new System.Drawing.Size(206, 613);
      this.gameBox.TabIndex = 2;
      this.gameBox.TabStop = false;
      // 
      // groupBox4
      // 
      this.groupBox4.Location = new System.Drawing.Point(13, 227);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(92, 399);
      this.groupBox4.TabIndex = 3;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Your Ad Here";
      // 
      // GameForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(323, 631);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.gameBox);
      this.Controls.Add(this.scoreBox);
      this.Controls.Add(this.predictionBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "GameForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "GameForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameForm_KeyPress);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
      this.scoreBox.ResumeLayout(false);
      this.scoreBox.PerformLayout();
      this.ResumeLayout(false);

      }

    #endregion

    private System.Windows.Forms.GroupBox predictionBox;
    private System.Windows.Forms.GroupBox scoreBox;
    private System.Windows.Forms.Label ScoreDisplay;
    private System.Windows.Forms.GroupBox gameBox;
    private System.Windows.Forms.GroupBox groupBox4;
    }
  }