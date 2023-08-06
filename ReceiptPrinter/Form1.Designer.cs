namespace ReceiptPrinter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            listBox1 = new ListBox();
            label1 = new Label();
            button1 = new Button();
            checkBox1 = new CheckBox();
            button2 = new Button();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printButton = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(6, 86);
            tabControl1.Margin = new Padding(2, 1, 2, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(636, 227);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(2, 1, 2, 1);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(2, 1, 2, 1);
            tabPage1.Size = new Size(628, 199);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Log";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 32;
            listBox1.Location = new Point(3, 3);
            listBox1.Margin = new Padding(2, 1, 2, 1);
            listBox1.Name = "listBox1";
            listBox1.ScrollAlwaysVisible = true;
            listBox1.Size = new Size(623, 196);
            listBox1.TabIndex = 0;
            listBox1.DrawItem += listBox1_DrawItem;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(457, 315);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(167, 15);
            label1.TabIndex = 1;
            label1.Text = "Copyright (C) Carl Sparks 2023";
            // 
            // button1
            // 
            button1.Location = new Point(558, 34);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(81, 22);
            button1.TabIndex = 2;
            button1.Text = "Test Print";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(542, 75);
            checkBox1.Margin = new Padding(2, 1, 2, 1);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(92, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Double Print";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(432, 34);
            button2.Margin = new Padding(2, 1, 2, 1);
            button2.Name = "button2";
            button2.Size = new Size(81, 22);
            button2.TabIndex = 4;
            button2.Text = "Start HTTP";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printButton
            // 
            printButton.Location = new Point(258, 33);
            printButton.Name = "printButton";
            printButton.Size = new Size(108, 23);
            printButton.TabIndex = 5;
            printButton.Text = "Print Button";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += printButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(649, 340);
            Controls.Add(printButton);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(tabControl1);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Form1";
            Text = "POS Receipt Printer";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label1;
        private Button button1;
        private CheckBox checkBox1;
        private Button button2;
        public ListBox listBox1;
        private Button printButton;
        public System.Drawing.Printing.PrintDocument printDocument1;
    }
}