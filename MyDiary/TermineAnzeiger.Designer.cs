namespace MyDiary
{
    partial class TermineAnzeiger
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
            this.lv_termine = new System.Windows.Forms.ListView();
            this.gb_termine = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_minuten = new System.Windows.Forms.TextBox();
            this.txtBox_stunden = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_titel = new System.Windows.Forms.TextBox();
            this.btn_termineHinzufuegen = new System.Windows.Forms.Button();
            this.btn_termineLoeschen = new System.Windows.Forms.Button();
            this.lv_abgelaufeneTermine = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gb_termine.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_termine
            // 
            this.lv_termine.HideSelection = false;
            this.lv_termine.Location = new System.Drawing.Point(12, 104);
            this.lv_termine.Name = "lv_termine";
            this.lv_termine.Size = new System.Drawing.Size(515, 498);
            this.lv_termine.TabIndex = 0;
            this.lv_termine.UseCompatibleStateImageBehavior = false;
            this.lv_termine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClick_lv_termine);
            // 
            // gb_termine
            // 
            this.gb_termine.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gb_termine.Controls.Add(this.label4);
            this.gb_termine.Controls.Add(this.label3);
            this.gb_termine.Controls.Add(this.label2);
            this.gb_termine.Controls.Add(this.txtBox_minuten);
            this.gb_termine.Controls.Add(this.txtBox_stunden);
            this.gb_termine.Controls.Add(this.label1);
            this.gb_termine.Controls.Add(this.txtBox_titel);
            this.gb_termine.Controls.Add(this.btn_termineHinzufuegen);
            this.gb_termine.Controls.Add(this.btn_termineLoeschen);
            this.gb_termine.Font = new System.Drawing.Font("Perpetua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_termine.Location = new System.Drawing.Point(533, 104);
            this.gb_termine.Name = "gb_termine";
            this.gb_termine.Size = new System.Drawing.Size(530, 498);
            this.gb_termine.TabIndex = 1;
            this.gb_termine.TabStop = false;
            this.gb_termine.Text = "Einen neuen Termin";
            this.gb_termine.Enter += new System.EventHandler(this.gb_termine_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(85, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "h";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = ":";
            // 
            // txtBox_minuten
            // 
            this.txtBox_minuten.Location = new System.Drawing.Point(348, 265);
            this.txtBox_minuten.Name = "txtBox_minuten";
            this.txtBox_minuten.Size = new System.Drawing.Size(93, 30);
            this.txtBox_minuten.TabIndex = 6;
            // 
            // txtBox_stunden
            // 
            this.txtBox_stunden.Location = new System.Drawing.Point(89, 264);
            this.txtBox_stunden.Name = "txtBox_stunden";
            this.txtBox_stunden.Size = new System.Drawing.Size(106, 30);
            this.txtBox_stunden.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Titel";
            // 
            // txtBox_titel
            // 
            this.txtBox_titel.Location = new System.Drawing.Point(33, 108);
            this.txtBox_titel.Multiline = true;
            this.txtBox_titel.Name = "txtBox_titel";
            this.txtBox_titel.Size = new System.Drawing.Size(454, 51);
            this.txtBox_titel.TabIndex = 3;
            // 
            // btn_termineHinzufuegen
            // 
            this.btn_termineHinzufuegen.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_termineHinzufuegen.Location = new System.Drawing.Point(311, 439);
            this.btn_termineHinzufuegen.Name = "btn_termineHinzufuegen";
            this.btn_termineHinzufuegen.Size = new System.Drawing.Size(176, 34);
            this.btn_termineHinzufuegen.TabIndex = 1;
            this.btn_termineHinzufuegen.Text = "Termin hinzufügen";
            this.btn_termineHinzufuegen.UseVisualStyleBackColor = false;
            this.btn_termineHinzufuegen.Click += new System.EventHandler(this.btn_termineHinzufuegen_Click);
            // 
            // btn_termineLoeschen
            // 
            this.btn_termineLoeschen.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_termineLoeschen.Location = new System.Drawing.Point(33, 439);
            this.btn_termineLoeschen.Name = "btn_termineLoeschen";
            this.btn_termineLoeschen.Size = new System.Drawing.Size(198, 34);
            this.btn_termineLoeschen.TabIndex = 0;
            this.btn_termineLoeschen.Text = "Termin löschen";
            this.btn_termineLoeschen.UseVisualStyleBackColor = false;
            this.btn_termineLoeschen.Click += new System.EventHandler(this.btn_termineLoeschen_Click);
            // 
            // lv_abgelaufeneTermine
            // 
            this.lv_abgelaufeneTermine.HideSelection = false;
            this.lv_abgelaufeneTermine.Location = new System.Drawing.Point(1069, 104);
            this.lv_abgelaufeneTermine.Name = "lv_abgelaufeneTermine";
            this.lv_abgelaufeneTermine.Size = new System.Drawing.Size(530, 498);
            this.lv_abgelaufeneTermine.TabIndex = 2;
            this.lv_abgelaufeneTermine.UseCompatibleStateImageBehavior = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Perpetua", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(46, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(405, 38);
            this.label5.TabIndex = 3;
            this.label5.Text = "Liste der vorgemerkten Termine";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Perpetua", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1137, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(392, 38);
            this.label6.TabIndex = 4;
            this.label6.Text = "Liste der abgelaufenen Termine";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // TermineAnzeiger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1611, 614);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lv_abgelaufeneTermine);
            this.Controls.Add(this.gb_termine);
            this.Controls.Add(this.lv_termine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TermineAnzeiger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Meine Termine";
            this.Load += new System.EventHandler(this.Termine_Load);
            this.gb_termine.ResumeLayout(false);
            this.gb_termine.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_termine;
        private System.Windows.Forms.GroupBox gb_termine;
        private System.Windows.Forms.Button btn_termineHinzufuegen;
        private System.Windows.Forms.Button btn_termineLoeschen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBox_titel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBox_minuten;
        private System.Windows.Forms.TextBox txtBox_stunden;
        private System.Windows.Forms.ListView lv_abgelaufeneTermine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}