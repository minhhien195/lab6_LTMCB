namespace _22520415_Doanso
{
    partial class CreateGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateserver = new System.Windows.Forms.Button();
            this.btnJoingame = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateserver
            // 
            this.btnCreateserver.AutoSize = true;
            this.btnCreateserver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCreateserver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateserver.Location = new System.Drawing.Point(378, 128);
            this.btnCreateserver.Name = "btnCreateserver";
            this.btnCreateserver.Size = new System.Drawing.Size(187, 64);
            this.btnCreateserver.TabIndex = 0;
            this.btnCreateserver.Text = "Tạo server game";
            this.btnCreateserver.UseVisualStyleBackColor = false;
            this.btnCreateserver.Click += new System.EventHandler(this.btnCreateserver_Click);
            // 
            // btnJoingame
            // 
            this.btnJoingame.AutoSize = true;
            this.btnJoingame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnJoingame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoingame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoingame.Location = new System.Drawing.Point(58, 128);
            this.btnJoingame.Name = "btnJoingame";
            this.btnJoingame.Size = new System.Drawing.Size(185, 64);
            this.btnJoingame.TabIndex = 1;
            this.btnJoingame.Text = "Tham gia game";
            this.btnJoingame.UseVisualStyleBackColor = false;
            this.btnJoingame.Click += new System.EventHandler(this.btnJoingame_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(593, 23);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 34);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(696, 358);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnJoingame);
            this.Controls.Add(this.btnCreateserver);
            this.Name = "CreateGame";
            this.Text = "CreateGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateserver;
        private System.Windows.Forms.Button btnJoingame;
        private System.Windows.Forms.Button btnExit;
    }
}