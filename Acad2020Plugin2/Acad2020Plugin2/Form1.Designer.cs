namespace Acad2020Plugin2
{
    partial class PlovniPutApp
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
            this.fStacionaze = new System.Windows.Forms.NumericUpDown();
            this.IzradiPero = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OdabirPera = new System.Windows.Forms.ComboBox();
            this.labelRotacija = new System.Windows.Forms.Label();
            this.fKutRotacije = new System.Windows.Forms.NumericUpDown();
            this.dKrak = new System.Windows.Forms.Label();
            this.fDuljinaKrakaD = new System.Windows.Forms.NumericUpDown();
            this.fDuljinaKrakaK = new System.Windows.Forms.NumericUpDown();
            this.kKrak = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.fDuljinaPera = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.fMaxDuljina = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.fStrana = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fStacionaze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKutRotacije)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaKrakaD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaKrakaK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaPera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // fStacionaze
            // 
            this.fStacionaze.DecimalPlaces = 2;
            this.fStacionaze.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fStacionaze.Location = new System.Drawing.Point(12, 31);
            this.fStacionaze.Maximum = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.fStacionaze.Name = "fStacionaze";
            this.fStacionaze.Size = new System.Drawing.Size(120, 20);
            this.fStacionaze.TabIndex = 0;
            this.fStacionaze.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IzradiPero
            // 
            this.IzradiPero.Location = new System.Drawing.Point(337, 243);
            this.IzradiPero.Name = "IzradiPero";
            this.IzradiPero.Size = new System.Drawing.Size(86, 45);
            this.IzradiPero.TabIndex = 1;
            this.IzradiPero.Text = "Izradi pero";
            this.IzradiPero.UseVisualStyleBackColor = true;
            this.IzradiPero.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Odaberite stacionažu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Odaberite vrstu pera";
            // 
            // OdabirPera
            // 
            this.OdabirPera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OdabirPera.FormattingEnabled = true;
            this.OdabirPera.Items.AddRange(new object[] {
            "T-pero",
            "Inklinirajuće/Deklinirajuće pero",
            "Okomito pero",
            "EcoPero"});
            this.OdabirPera.Location = new System.Drawing.Point(12, 75);
            this.OdabirPera.Name = "OdabirPera";
            this.OdabirPera.Size = new System.Drawing.Size(121, 21);
            this.OdabirPera.TabIndex = 4;
            // 
            // labelRotacija
            // 
            this.labelRotacija.AutoSize = true;
            this.labelRotacija.Location = new System.Drawing.Point(12, 252);
            this.labelRotacija.Name = "labelRotacija";
            this.labelRotacija.Size = new System.Drawing.Size(60, 13);
            this.labelRotacija.TabIndex = 5;
            this.labelRotacija.Text = "Kut rotacije";
            // 
            // fKutRotacije
            // 
            this.fKutRotacije.DecimalPlaces = 2;
            this.fKutRotacije.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.fKutRotacije.Location = new System.Drawing.Point(15, 268);
            this.fKutRotacije.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.fKutRotacije.Name = "fKutRotacije";
            this.fKutRotacije.Size = new System.Drawing.Size(69, 20);
            this.fKutRotacije.TabIndex = 6;
            this.fKutRotacije.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dKrak
            // 
            this.dKrak.AutoSize = true;
            this.dKrak.Location = new System.Drawing.Point(7, 204);
            this.dKrak.Name = "dKrak";
            this.dKrak.Size = new System.Drawing.Size(101, 13);
            this.dKrak.TabIndex = 7;
            this.dKrak.Text = "Duljina dužeg kraka";
            // 
            // fDuljinaKrakaD
            // 
            this.fDuljinaKrakaD.DecimalPlaces = 2;
            this.fDuljinaKrakaD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fDuljinaKrakaD.Location = new System.Drawing.Point(10, 222);
            this.fDuljinaKrakaD.Name = "fDuljinaKrakaD";
            this.fDuljinaKrakaD.Size = new System.Drawing.Size(74, 20);
            this.fDuljinaKrakaD.TabIndex = 8;
            this.fDuljinaKrakaD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fDuljinaKrakaK
            // 
            this.fDuljinaKrakaK.DecimalPlaces = 2;
            this.fDuljinaKrakaK.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fDuljinaKrakaK.Location = new System.Drawing.Point(141, 222);
            this.fDuljinaKrakaK.Name = "fDuljinaKrakaK";
            this.fDuljinaKrakaK.Size = new System.Drawing.Size(74, 20);
            this.fDuljinaKrakaK.TabIndex = 9;
            this.fDuljinaKrakaK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // kKrak
            // 
            this.kKrak.AutoSize = true;
            this.kKrak.Location = new System.Drawing.Point(138, 204);
            this.kKrak.Name = "kKrak";
            this.kKrak.Size = new System.Drawing.Size(105, 13);
            this.kKrak.TabIndex = 10;
            this.kKrak.Text = "Duljina kračeg kraka";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(226, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(119, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "m";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(138, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Duljina pera";
            // 
            // fDuljinaPera
            // 
            this.fDuljinaPera.DecimalPlaces = 2;
            this.fDuljinaPera.Location = new System.Drawing.Point(12, 120);
            this.fDuljinaPera.Name = "fDuljinaPera";
            this.fDuljinaPera.Size = new System.Drawing.Size(101, 20);
            this.fDuljinaPera.TabIndex = 15;
            this.fDuljinaPera.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Maksimalna duljina";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(91, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 16);
            this.label8.TabIndex = 17;
            this.label8.Text = "m";
            // 
            // fMaxDuljina
            // 
            this.fMaxDuljina.AutoSize = true;
            this.fMaxDuljina.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fMaxDuljina.Location = new System.Drawing.Point(30, 160);
            this.fMaxDuljina.Name = "fMaxDuljina";
            this.fMaxDuljina.Size = new System.Drawing.Size(38, 16);
            this.fMaxDuljina.TabIndex = 18;
            this.fMaxDuljina.Text = "0.00 m";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(138, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Nagib pera";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(161, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 164);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown2.Location = new System.Drawing.Point(141, 268);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown2.TabIndex = 21;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fStrana
            // 
            this.fStrana.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fStrana.FormattingEnabled = true;
            this.fStrana.Items.AddRange(new object[] {
            "Lijevo",
            "Desno"});
            this.fStrana.Location = new System.Drawing.Point(302, 216);
            this.fStrana.Name = "fStrana";
            this.fStrana.Size = new System.Drawing.Size(121, 21);
            this.fStrana.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Odaberite stranu";
            // 
            // PlovniPutApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 309);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.fStrana);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.fMaxDuljina);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.fDuljinaPera);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.kKrak);
            this.Controls.Add(this.fDuljinaKrakaK);
            this.Controls.Add(this.fDuljinaKrakaD);
            this.Controls.Add(this.dKrak);
            this.Controls.Add(this.fKutRotacije);
            this.Controls.Add(this.labelRotacija);
            this.Controls.Add(this.OdabirPera);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IzradiPero);
            this.Controls.Add(this.fStacionaze);
            this.MaximizeBox = false;
            this.Name = "PlovniPutApp";
            this.Text = "Plovni put APP";
            ((System.ComponentModel.ISupportInitialize)(this.fStacionaze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKutRotacije)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaKrakaD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaKrakaK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fDuljinaPera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown fStacionaze;
        private System.Windows.Forms.Button IzradiPero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox OdabirPera;
        private System.Windows.Forms.Label labelRotacija;
        private System.Windows.Forms.NumericUpDown fKutRotacije;
        private System.Windows.Forms.Label dKrak;
        private System.Windows.Forms.NumericUpDown fDuljinaKrakaD;
        private System.Windows.Forms.NumericUpDown fDuljinaKrakaK;
        private System.Windows.Forms.Label kKrak;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown fDuljinaPera;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label fMaxDuljina;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.ComboBox fStrana;
        private System.Windows.Forms.Label label10;
    }
}