namespace SlikaPrepoznavanjeKarata
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
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.buttonLoadPicture = new System.Windows.Forms.Button();
            this.pathToPicture = new System.Windows.Forms.TextBox();
            this.filter = new System.Windows.Forms.Button();
            this.blueComponent = new System.Windows.Forms.TextBox();
            this.greenComponent = new System.Windows.Forms.TextBox();
            this.redComponent = new System.Windows.Forms.TextBox();
            this.blueCoeficiente = new System.Windows.Forms.TextBox();
            this.greenCoeficiente = new System.Windows.Forms.TextBox();
            this.redCoeficiente = new System.Windows.Forms.TextBox();
            this.btnToGray = new System.Windows.Forms.Button();
            this.btnThreshold = new System.Windows.Forms.Button();
            this.thresholdMin = new System.Windows.Forms.TextBox();
            this.thresholdMax = new System.Windows.Forms.TextBox();
            this.btnDilate = new System.Windows.Forms.Button();
            this.btnErode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnKNN = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.prviParametarHough = new System.Windows.Forms.TextBox();
            this.drugiParametarHough = new System.Windows.Forms.TextBox();
            this.treciParametarHough = new System.Windows.Forms.TextBox();
            this.cetvrtiParametarHough = new System.Windows.Forms.TextBox();
            this.petiParametarHough = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.imeSlikeTextBox = new System.Windows.Forms.TextBox();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.knnTest1textBox = new System.Windows.Forms.TextBox();
            this.button17 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(1297, 532);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // buttonLoadPicture
            // 
            this.buttonLoadPicture.Location = new System.Drawing.Point(12, 605);
            this.buttonLoadPicture.Name = "buttonLoadPicture";
            this.buttonLoadPicture.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadPicture.TabIndex = 3;
            this.buttonLoadPicture.Tag = "";
            this.buttonLoadPicture.Text = "Load picture";
            this.buttonLoadPicture.UseVisualStyleBackColor = true;
            this.buttonLoadPicture.Click += new System.EventHandler(this.buttonLoadPicture_Click);
            // 
            // pathToPicture
            // 
            this.pathToPicture.Location = new System.Drawing.Point(93, 607);
            this.pathToPicture.Name = "pathToPicture";
            this.pathToPicture.Size = new System.Drawing.Size(210, 20);
            this.pathToPicture.TabIndex = 4;
            // 
            // filter
            // 
            this.filter.Location = new System.Drawing.Point(570, 605);
            this.filter.Name = "filter";
            this.filter.Size = new System.Drawing.Size(75, 23);
            this.filter.TabIndex = 5;
            this.filter.Text = "Primeni filter";
            this.filter.UseVisualStyleBackColor = true;
            this.filter.Click += new System.EventHandler(this.button1_Click);
            // 
            // blueComponent
            // 
            this.blueComponent.Location = new System.Drawing.Point(309, 607);
            this.blueComponent.Name = "blueComponent";
            this.blueComponent.Size = new System.Drawing.Size(43, 20);
            this.blueComponent.TabIndex = 6;
            this.blueComponent.Text = "100,255";
            // 
            // greenComponent
            // 
            this.greenComponent.Location = new System.Drawing.Point(358, 607);
            this.greenComponent.Name = "greenComponent";
            this.greenComponent.Size = new System.Drawing.Size(100, 20);
            this.greenComponent.TabIndex = 7;
            this.greenComponent.Text = "100,255";
            // 
            // redComponent
            // 
            this.redComponent.Location = new System.Drawing.Point(464, 607);
            this.redComponent.Name = "redComponent";
            this.redComponent.Size = new System.Drawing.Size(100, 20);
            this.redComponent.TabIndex = 8;
            this.redComponent.Text = "100,255";
            // 
            // blueCoeficiente
            // 
            this.blueCoeficiente.Location = new System.Drawing.Point(309, 644);
            this.blueCoeficiente.Name = "blueCoeficiente";
            this.blueCoeficiente.Size = new System.Drawing.Size(43, 20);
            this.blueCoeficiente.TabIndex = 9;
            this.blueCoeficiente.Text = "0.5";
            // 
            // greenCoeficiente
            // 
            this.greenCoeficiente.Location = new System.Drawing.Point(358, 644);
            this.greenCoeficiente.Name = "greenCoeficiente";
            this.greenCoeficiente.Size = new System.Drawing.Size(100, 20);
            this.greenCoeficiente.TabIndex = 10;
            this.greenCoeficiente.Text = "0.5";
            // 
            // redCoeficiente
            // 
            this.redCoeficiente.Location = new System.Drawing.Point(464, 644);
            this.redCoeficiente.Name = "redCoeficiente";
            this.redCoeficiente.Size = new System.Drawing.Size(100, 20);
            this.redCoeficiente.TabIndex = 11;
            this.redCoeficiente.Text = "0";
            // 
            // btnToGray
            // 
            this.btnToGray.Location = new System.Drawing.Point(570, 641);
            this.btnToGray.Name = "btnToGray";
            this.btnToGray.Size = new System.Drawing.Size(75, 23);
            this.btnToGray.TabIndex = 12;
            this.btnToGray.Text = "toGray";
            this.btnToGray.UseVisualStyleBackColor = true;
            this.btnToGray.Click += new System.EventHandler(this.btnToGray_Click);
            // 
            // btnThreshold
            // 
            this.btnThreshold.Location = new System.Drawing.Point(970, 604);
            this.btnThreshold.Name = "btnThreshold";
            this.btnThreshold.Size = new System.Drawing.Size(75, 23);
            this.btnThreshold.TabIndex = 13;
            this.btnThreshold.Text = "Threshold";
            this.btnThreshold.UseVisualStyleBackColor = true;
            this.btnThreshold.Click += new System.EventHandler(this.btnThreshold_Click);
            // 
            // thresholdMin
            // 
            this.thresholdMin.Location = new System.Drawing.Point(758, 608);
            this.thresholdMin.Name = "thresholdMin";
            this.thresholdMin.Size = new System.Drawing.Size(100, 20);
            this.thresholdMin.TabIndex = 14;
            this.thresholdMin.Text = "254";
            // 
            // thresholdMax
            // 
            this.thresholdMax.Location = new System.Drawing.Point(864, 608);
            this.thresholdMax.Name = "thresholdMax";
            this.thresholdMax.Size = new System.Drawing.Size(100, 20);
            this.thresholdMax.TabIndex = 15;
            this.thresholdMax.Text = "255";
            // 
            // btnDilate
            // 
            this.btnDilate.Location = new System.Drawing.Point(758, 641);
            this.btnDilate.Name = "btnDilate";
            this.btnDilate.Size = new System.Drawing.Size(75, 23);
            this.btnDilate.TabIndex = 16;
            this.btnDilate.Text = "Dilate";
            this.btnDilate.UseVisualStyleBackColor = true;
            this.btnDilate.Click += new System.EventHandler(this.btnDilate_Click);
            // 
            // btnErode
            // 
            this.btnErode.Location = new System.Drawing.Point(839, 641);
            this.btnErode.Name = "btnErode";
            this.btnErode.Size = new System.Drawing.Size(75, 23);
            this.btnErode.TabIndex = 17;
            this.btnErode.Text = "Erode";
            this.btnErode.UseVisualStyleBackColor = true;
            this.btnErode.Click += new System.EventHandler(this.btnErode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1147, 641);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Post";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnKNN
            // 
            this.btnKNN.Location = new System.Drawing.Point(1095, 642);
            this.btnKNN.Name = "btnKNN";
            this.btnKNN.Size = new System.Drawing.Size(46, 23);
            this.btnKNN.TabIndex = 19;
            this.btnKNN.Text = "KNN";
            this.btnKNN.UseVisualStyleBackColor = true;
            this.btnKNN.Click += new System.EventHandler(this.btnKNN_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(663, 606);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(663, 642);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "cetvorouglovi";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(93, 643);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 22;
            this.button4.Text = "Conture";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 643);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 23;
            this.button5.Text = "izvod";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1214, 550);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 24;
            this.button6.Text = "houghlines";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(920, 641);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(81, 23);
            this.button7.TabIndex = 25;
            this.button7.Text = "rastavljanje";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(171, 644);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(132, 23);
            this.button8.TabIndex = 26;
            this.button8.Text = "ucitavnje crno belih slika";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1214, 641);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(95, 23);
            this.button9.TabIndex = 27;
            this.button9.Text = "Jenacine prave";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1240, 604);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 28;
            this.button10.Text = "Perspektiva";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // prviParametarHough
            // 
            this.prviParametarHough.Location = new System.Drawing.Point(683, 553);
            this.prviParametarHough.Name = "prviParametarHough";
            this.prviParametarHough.Size = new System.Drawing.Size(100, 20);
            this.prviParametarHough.TabIndex = 29;
            // 
            // drugiParametarHough
            // 
            this.drugiParametarHough.Location = new System.Drawing.Point(789, 553);
            this.drugiParametarHough.Name = "drugiParametarHough";
            this.drugiParametarHough.Size = new System.Drawing.Size(100, 20);
            this.drugiParametarHough.TabIndex = 30;
            // 
            // treciParametarHough
            // 
            this.treciParametarHough.Location = new System.Drawing.Point(895, 553);
            this.treciParametarHough.Name = "treciParametarHough";
            this.treciParametarHough.Size = new System.Drawing.Size(100, 20);
            this.treciParametarHough.TabIndex = 31;
            // 
            // cetvrtiParametarHough
            // 
            this.cetvrtiParametarHough.Location = new System.Drawing.Point(1001, 553);
            this.cetvrtiParametarHough.Name = "cetvrtiParametarHough";
            this.cetvrtiParametarHough.Size = new System.Drawing.Size(100, 20);
            this.cetvrtiParametarHough.TabIndex = 32;
            // 
            // petiParametarHough
            // 
            this.petiParametarHough.Location = new System.Drawing.Point(1108, 553);
            this.petiParametarHough.Name = "petiParametarHough";
            this.petiParametarHough.Size = new System.Drawing.Size(100, 20);
            this.petiParametarHough.TabIndex = 33;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(12, 576);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 34;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1188, 602);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(46, 23);
            this.button12.TabIndex = 35;
            this.button12.Text = "save";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // imeSlikeTextBox
            // 
            this.imeSlikeTextBox.Location = new System.Drawing.Point(1052, 605);
            this.imeSlikeTextBox.Name = "imeSlikeTextBox";
            this.imeSlikeTextBox.Size = new System.Drawing.Size(130, 20);
            this.imeSlikeTextBox.TabIndex = 36;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(190, 553);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(333, 23);
            this.button13.TabIndex = 37;
            this.button13.Text = "Final sekvence";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1007, 642);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(82, 23);
            this.button14.TabIndex = 38;
            this.button14.Text = "KNN Thread";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1066, 579);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(116, 23);
            this.button15.TabIndex = 39;
            this.button15.Text = "button15";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(748, 579);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 40;
            this.button16.Text = "KNN_TEST";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // knnTest1textBox
            // 
            this.knnTest1textBox.Location = new System.Drawing.Point(560, 578);
            this.knnTest1textBox.Name = "knnTest1textBox";
            this.knnTest1textBox.Size = new System.Drawing.Size(182, 20);
            this.knnTest1textBox.TabIndex = 41;
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(829, 579);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(135, 23);
            this.button17.TabIndex = 42;
            this.button17.Text = "KNNTESTCOLOR";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1321, 680);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.knnTest1textBox);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.imeSlikeTextBox);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.petiParametarHough);
            this.Controls.Add(this.cetvrtiParametarHough);
            this.Controls.Add(this.treciParametarHough);
            this.Controls.Add(this.drugiParametarHough);
            this.Controls.Add(this.prviParametarHough);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnKNN);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnErode);
            this.Controls.Add(this.btnDilate);
            this.Controls.Add(this.thresholdMax);
            this.Controls.Add(this.thresholdMin);
            this.Controls.Add(this.btnThreshold);
            this.Controls.Add(this.btnToGray);
            this.Controls.Add(this.redCoeficiente);
            this.Controls.Add(this.greenCoeficiente);
            this.Controls.Add(this.blueCoeficiente);
            this.Controls.Add(this.redComponent);
            this.Controls.Add(this.greenComponent);
            this.Controls.Add(this.blueComponent);
            this.Controls.Add(this.filter);
            this.Controls.Add(this.pathToPicture);
            this.Controls.Add(this.buttonLoadPicture);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button buttonLoadPicture;
        private System.Windows.Forms.TextBox pathToPicture;
        private System.Windows.Forms.Button filter;
        private System.Windows.Forms.TextBox blueComponent;
        private System.Windows.Forms.TextBox greenComponent;
        private System.Windows.Forms.TextBox redComponent;
        private System.Windows.Forms.TextBox blueCoeficiente;
        private System.Windows.Forms.TextBox greenCoeficiente;
        private System.Windows.Forms.TextBox redCoeficiente;
        private System.Windows.Forms.Button btnToGray;
        private System.Windows.Forms.Button btnThreshold;
        private System.Windows.Forms.TextBox thresholdMin;
        private System.Windows.Forms.TextBox thresholdMax;
        private System.Windows.Forms.Button btnDilate;
        private System.Windows.Forms.Button btnErode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnKNN;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox prviParametarHough;
        private System.Windows.Forms.TextBox drugiParametarHough;
        private System.Windows.Forms.TextBox treciParametarHough;
        private System.Windows.Forms.TextBox cetvrtiParametarHough;
        private System.Windows.Forms.TextBox petiParametarHough;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox imeSlikeTextBox;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TextBox knnTest1textBox;
        private System.Windows.Forms.Button button17;
    }
}

