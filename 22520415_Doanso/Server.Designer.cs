namespace _22520415_Doanso
{
    partial class Server
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
            this.components = new System.ComponentModel.Container();
            this.rtbMess = new System.Windows.Forms.RichTextBox();
            this.lbPhamvi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.lbRange = new System.Windows.Forms.Label();
            this.lbPlayers = new System.Windows.Forms.Label();
            this.lbRound = new System.Windows.Forms.Label();
            this.tmCount = new System.Windows.Forms.Timer(this.components);
            this.lbCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbMess
            // 
            this.rtbMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMess.Location = new System.Drawing.Point(21, 12);
            this.rtbMess.Name = "rtbMess";
            this.rtbMess.Size = new System.Drawing.Size(757, 409);
            this.rtbMess.TabIndex = 0;
            this.rtbMess.Text = "";
            // 
            // lbPhamvi
            // 
            this.lbPhamvi.AutoSize = true;
            this.lbPhamvi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPhamvi.Location = new System.Drawing.Point(795, 257);
            this.lbPhamvi.Name = "lbPhamvi";
            this.lbPhamvi.Size = new System.Drawing.Size(200, 25);
            this.lbPhamvi.TabIndex = 1;
            this.lbPhamvi.Text = "Phạm vi số cần tìm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(793, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Số cần tìm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(793, 325);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số người tham gia:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(793, 393);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số vòng:";
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumber.Location = new System.Drawing.Point(920, 189);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(36, 25);
            this.lbNumber.TabIndex = 5;
            this.lbNumber.Text = "23";
            // 
            // lbRange
            // 
            this.lbRange.AutoSize = true;
            this.lbRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRange.Location = new System.Drawing.Point(1001, 257);
            this.lbRange.Name = "lbRange";
            this.lbRange.Size = new System.Drawing.Size(98, 25);
            this.lbRange.TabIndex = 6;
            this.lbRange.Text = "[ 15, 59 ]";
            // 
            // lbPlayers
            // 
            this.lbPlayers.AutoSize = true;
            this.lbPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPlayers.Location = new System.Drawing.Point(1001, 325);
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(24, 25);
            this.lbPlayers.TabIndex = 7;
            this.lbPlayers.Text = "2";
            // 
            // lbRound
            // 
            this.lbRound.AutoSize = true;
            this.lbRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRound.Location = new System.Drawing.Point(898, 394);
            this.lbRound.Name = "lbRound";
            this.lbRound.Size = new System.Drawing.Size(43, 25);
            this.lbRound.TabIndex = 8;
            this.lbRound.Text = "2/5";
            // 
            // tmCount
            // 
            this.tmCount.Enabled = true;
            this.tmCount.Interval = 1000;
            this.tmCount.Tick += new System.EventHandler(this.tmCount_Tick);
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCount.Location = new System.Drawing.Point(885, 35);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(153, 108);
            this.lbCount.TabIndex = 19;
            this.lbCount.Text = "00";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1145, 445);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbRound);
            this.Controls.Add(this.lbPlayers);
            this.Controls.Add(this.lbRange);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPhamvi);
            this.Controls.Add(this.rtbMess);
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMess;
        private System.Windows.Forms.Label lbPhamvi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.Label lbRange;
        private System.Windows.Forms.Label lbPlayers;
        private System.Windows.Forms.Label lbRound;
        private System.Windows.Forms.Timer tmCount;
        private System.Windows.Forms.Label lbCount;
    }
}

