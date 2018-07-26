namespace PathPlanner
{
    partial class GraphViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"> True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.FromBox = new System.Windows.Forms.TextBox();
            this.ToBox = new System.Windows.Forms.TextBox();
            this.FindPathButton = new System.Windows.Forms.Button();
            this.AStar = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.ReadFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FromBox
            // 
            this.FromBox.Location = new System.Drawing.Point(17, 16);
            this.FromBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.FromBox.Name = "FromBox";
            this.FromBox.Size = new System.Drawing.Size(260, 38);
            this.FromBox.TabIndex = 0;
            this.FromBox.Text = "From";
            this.FromBox.TextChanged += new System.EventHandler(this.FromBoxTextChanged);
            // 
            // ToBox
            // 
            this.ToBox.Location = new System.Drawing.Point(293, 16);
            this.ToBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ToBox.Name = "ToBox";
            this.ToBox.Size = new System.Drawing.Size(260, 38);
            this.ToBox.TabIndex = 1;
            this.ToBox.Text = "To";
            this.ToBox.TextChanged += new System.EventHandler(this.ToBoxTextChanged);
            // 
            // FindPathButton
            // 
            this.FindPathButton.Location = new System.Drawing.Point(569, 16);
            this.FindPathButton.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.FindPathButton.Name = "FindPathButton";
            this.FindPathButton.Size = new System.Drawing.Size(200, 55);
            this.FindPathButton.TabIndex = 2;
            this.FindPathButton.Text = "Dijkstra";
            this.FindPathButton.UseVisualStyleBackColor = true;
            this.FindPathButton.Click += new System.EventHandler(this.FindPathButtonClick);
            // 
            // AStar
            // 
            this.AStar.Location = new System.Drawing.Point(785, 16);
            this.AStar.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.AStar.Name = "AStar";
            this.AStar.Size = new System.Drawing.Size(200, 55);
            this.AStar.TabIndex = 3;
            this.AStar.Text = "A*";
            this.AStar.UseVisualStyleBackColor = true;
            this.AStar.Click += new System.EventHandler(this.AStarClick);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(10, 85);
            this.FilePathTextBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(543, 38);
            this.FilePathTextBox.TabIndex = 4;
            this.FilePathTextBox.Text = "FilePath relative to ./bin/debug";
            // 
            // ReadFile
            // 
            this.ReadFile.Location = new System.Drawing.Point(569, 85);
            this.ReadFile.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ReadFile.Name = "ReadFile";
            this.ReadFile.Size = new System.Drawing.Size(200, 55);
            this.ReadFile.TabIndex = 5;
            this.ReadFile.Text = "Read File";
            this.ReadFile.UseVisualStyleBackColor = true;
            this.ReadFile.Click += new System.EventHandler(this.ReadFileClick);
            // 
            // GraphViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2008, 1469);
            this.Controls.Add(this.ReadFile);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.AStar);
            this.Controls.Add(this.FindPathButton);
            this.Controls.Add(this.ToBox);
            this.Controls.Add(this.FromBox);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "GraphViewer";
            this.Text = "Path Planner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FromBox;
        private System.Windows.Forms.TextBox ToBox;
        private System.Windows.Forms.Button FindPathButton;
        private System.Windows.Forms.Button AStar;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Button ReadFile;
    }
}

