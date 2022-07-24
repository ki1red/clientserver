namespace Client
{
    partial class Client
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.treeViewFiles = new System.Windows.Forms.TreeView();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.con_discon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Location = new System.Drawing.Point(230, 103);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(289, 265);
            this.richTextBoxMessages.TabIndex = 7;
            this.richTextBoxMessages.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(373, 12);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(146, 23);
            this.buttonDisconnect.TabIndex = 8;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click_1);
            // 
            // treeViewFiles
            // 
            this.treeViewFiles.Location = new System.Drawing.Point(16, 49);
            this.treeViewFiles.Name = "treeViewFiles";
            this.treeViewFiles.Size = new System.Drawing.Size(208, 319);
            this.treeViewFiles.TabIndex = 9;
            this.treeViewFiles.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFiles_BeforeExpand);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(16, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(208, 20);
            this.textBoxIP.TabIndex = 10;
            this.textBoxIP.Text = "TCP/IP";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(230, 49);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(137, 23);
            this.buttonSend.TabIndex = 14;
            this.buttonSend.Text = "Receive Data";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(373, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Load Disks";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button2_MouseClick);
            // 
            // con_discon
            // 
            this.con_discon.AutoSize = true;
            this.con_discon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.con_discon.Location = new System.Drawing.Point(332, 87);
            this.con_discon.Name = "con_discon";
            this.con_discon.Size = new System.Drawing.Size(73, 13);
            this.con_discon.TabIndex = 16;
            this.con_discon.Text = "Disconnected";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 380);
            this.Controls.Add(this.con_discon);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.treeViewFiles);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.richTextBoxMessages);
            this.Name = "Client";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.TreeView treeViewFiles;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label con_discon;
    }
}

