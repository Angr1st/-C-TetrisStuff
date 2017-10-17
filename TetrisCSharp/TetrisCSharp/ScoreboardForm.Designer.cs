namespace TetrisCSharp
  {
  partial class ScoreboardForm
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
      this.nameBox = new System.Windows.Forms.TextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.submitButton = new System.Windows.Forms.Button();
      this.backButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // nameBox
      // 
      this.nameBox.Location = new System.Drawing.Point(12, 50);
      this.nameBox.Name = "nameBox";
      this.nameBox.Size = new System.Drawing.Size(100, 20);
      this.nameBox.TabIndex = 0;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(13, 13);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(324, 13);
      this.descriptionLabel.TabIndex = 1;
      this.descriptionLabel.Text = "Gebe bitte den Namen der auf dem Scoreboard erscheinen soll ein.";
      // 
      // submitButton
      // 
      this.submitButton.Location = new System.Drawing.Point(140, 50);
      this.submitButton.Name = "submitButton";
      this.submitButton.Size = new System.Drawing.Size(75, 23);
      this.submitButton.TabIndex = 2;
      this.submitButton.Text = "Submit";
      this.submitButton.UseVisualStyleBackColor = true;
      this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
      // 
      // backButton
      // 
      this.backButton.Location = new System.Drawing.Point(221, 50);
      this.backButton.Name = "backButton";
      this.backButton.Size = new System.Drawing.Size(75, 23);
      this.backButton.TabIndex = 3;
      this.backButton.Text = "Back";
      this.backButton.UseVisualStyleBackColor = true;
      this.backButton.Click += new System.EventHandler(this.backButton_Click);
      // 
      // ScoreboardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(337, 86);
      this.Controls.Add(this.backButton);
      this.Controls.Add(this.submitButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.nameBox);
      this.Name = "ScoreboardForm";
      this.Text = "ScoreboardForm";
      this.Load += new System.EventHandler(this.ScoreboardForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

      }

    #endregion
    private System.Windows.Forms.TextBox nameBox;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.Button submitButton;
    private System.Windows.Forms.Button backButton;
    }
  }