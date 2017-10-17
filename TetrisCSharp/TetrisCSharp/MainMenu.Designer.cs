namespace TetrisCSharp
  {
  partial class MainMenu
    {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose( bool disposing )
      {
      if ( disposing && ( components != null ) )
        {
        components.Dispose();
        }
      base.Dispose( disposing );
      }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
      {
            this.StartButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.scoreBoardButton = new System.Windows.Forms.Button();
            this.musicToggleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(93, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(143, 42);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "&Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(126, 120);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "&Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // scoreBoardButton
            // 
            this.scoreBoardButton.Location = new System.Drawing.Point(126, 60);
            this.scoreBoardButton.Name = "scoreBoardButton";
            this.scoreBoardButton.Size = new System.Drawing.Size(75, 23);
            this.scoreBoardButton.TabIndex = 2;
            this.scoreBoardButton.Text = "Top Ten";
            this.scoreBoardButton.UseVisualStyleBackColor = true;
            this.scoreBoardButton.Click += new System.EventHandler(this.scoreBoardButton_Click);
            // 
            // musicToggleButton
            // 
            this.musicToggleButton.Location = new System.Drawing.Point(126, 90);
            this.musicToggleButton.Name = "musicToggleButton";
            this.musicToggleButton.Size = new System.Drawing.Size(75, 23);
            this.musicToggleButton.TabIndex = 3;
            this.musicToggleButton.Text = "Music Off";
            this.musicToggleButton.UseVisualStyleBackColor = true;
            this.musicToggleButton.Click += new System.EventHandler(this.musicToggleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 155);
            this.Controls.Add(this.musicToggleButton);
            this.Controls.Add(this.scoreBoardButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSharpTetris";
            this.ResumeLayout(false);

      }

    #endregion

    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button scoreBoardButton;
        private System.Windows.Forms.Button musicToggleButton;
    }
  }

