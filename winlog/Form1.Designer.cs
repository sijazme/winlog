namespace winlog
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ConsoleBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.BackColor = System.Drawing.Color.Black;
            this.ConsoleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConsoleBox.ForeColor = System.Drawing.Color.White;
            this.ConsoleBox.Location = new System.Drawing.Point(10, 8);
            this.ConsoleBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ConsoleBox.Size = new System.Drawing.Size(760, 395);
            this.ConsoleBox.TabIndex = 0;
            this.ConsoleBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 414);
            this.Controls.Add(this.ConsoleBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "Form1";
            this.Text = "Console";
            this.ResumeLayout(false);

        }

        delegate void PrintCallback(string text);
        
        public void Print(string text)
        {
            if (this.ConsoleBox.InvokeRequired)
            {
                PrintCallback d = new PrintCallback(Print);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(ConsoleBox.Text))
                {
                    ConsoleBox.AppendText("\r\n" + text);
                }
                else
                {
                    ConsoleBox.AppendText(text + "\r\n");
                }

                ConsoleBox.ScrollToCaret();
            }
        }


        delegate void ToggleCallback();
        public void Toggle()
        {
            if (this.InvokeRequired)
            {
                ToggleCallback d = new ToggleCallback(Toggle);
                this.Invoke(d, new object[] { });
            }
            else
            {

                this.Visible = !this.Visible;
            }
        }

        #endregion

        private System.Windows.Forms.RichTextBox ConsoleBox;
    }
}

