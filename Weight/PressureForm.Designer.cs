namespace PTool
{
    partial class PressureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PressureForm));
            this.tlpParameter = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDeviceNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BalanceSerialPort = new System.Windows.Forms.ComboBox();
            this.picConnectState = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.tlpTitle = new System.Windows.Forms.TableLayoutPanel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.picCloseWindow = new System.Windows.Forms.PictureBox();
            this.picSetting = new System.Windows.Forms.PictureBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZeroCali = new System.Windows.Forms.ToolStripMenuItem();
            this.mCalibration = new PTool.Calibration();
            this.tlpParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectState)).BeginInit();
            this.tlpTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCloseWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSetting)).BeginInit();
            this.tlpMain.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpParameter
            // 
            this.tlpParameter.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tlpParameter.ColumnCount = 4;
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpParameter.Controls.Add(this.btnSave, 2, 0);
            this.tlpParameter.Controls.Add(this.label2, 0, 0);
            this.tlpParameter.Controls.Add(this.tbDeviceNumber, 1, 0);
            this.tlpParameter.Controls.Add(this.label1, 0, 1);
            this.tlpParameter.Controls.Add(this.BalanceSerialPort, 1, 1);
            this.tlpParameter.Controls.Add(this.picConnectState, 2, 1);
            this.tlpParameter.Controls.Add(this.btnReset, 3, 0);
            this.tlpParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParameter.Location = new System.Drawing.Point(0, 35);
            this.tlpParameter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpParameter.Name = "tlpParameter";
            this.tlpParameter.RowCount = 2;
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpParameter.Size = new System.Drawing.Size(450, 67);
            this.tlpParameter.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(320, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "设备编号";
            // 
            // tbDeviceNumber
            // 
            this.tbDeviceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDeviceNumber.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbDeviceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.tbDeviceNumber.ForeColor = System.Drawing.Color.White;
            this.tbDeviceNumber.Location = new System.Drawing.Point(115, 3);
            this.tbDeviceNumber.MaxLength = 10;
            this.tbDeviceNumber.Name = "tbDeviceNumber";
            this.tbDeviceNumber.Size = new System.Drawing.Size(196, 26);
            this.tbDeviceNumber.TabIndex = 1;
            this.tbDeviceNumber.WordWrap = false;
            this.tbDeviceNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDeviceNumber_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(35, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口";
            // 
            // BalanceSerialPort
            // 
            this.BalanceSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BalanceSerialPort.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BalanceSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BalanceSerialPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.BalanceSerialPort.ForeColor = System.Drawing.Color.White;
            this.BalanceSerialPort.FormattingEnabled = true;
            this.BalanceSerialPort.Location = new System.Drawing.Point(115, 36);
            this.BalanceSerialPort.Name = "BalanceSerialPort";
            this.BalanceSerialPort.Size = new System.Drawing.Size(196, 28);
            this.BalanceSerialPort.TabIndex = 2;
            this.BalanceSerialPort.SelectedIndexChanged += new System.EventHandler(this.BalanceSerialPort_SelectedIndexChanged);
            // 
            // picConnectState
            // 
            this.picConnectState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picConnectState.BackColor = System.Drawing.SystemColors.HotTrack;
            this.picConnectState.ErrorImage = global::HangBalance.Properties.Resources.error;
            this.picConnectState.Image = global::HangBalance.Properties.Resources.error2;
            this.picConnectState.Location = new System.Drawing.Point(323, 42);
            this.picConnectState.Margin = new System.Windows.Forms.Padding(9);
            this.picConnectState.Name = "picConnectState";
            this.picConnectState.Size = new System.Drawing.Size(30, 16);
            this.picConnectState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picConnectState.TabIndex = 0;
            this.picConnectState.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(388, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(55, 25);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tlpTitle
            // 
            this.tlpTitle.ColumnCount = 5;
            this.tlpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpTitle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpTitle.Controls.Add(this.picLogo, 0, 0);
            this.tlpTitle.Controls.Add(this.lbTitle, 1, 0);
            this.tlpTitle.Controls.Add(this.picCloseWindow, 4, 0);
            this.tlpTitle.Controls.Add(this.picSetting, 3, 0);
            this.tlpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTitle.Location = new System.Drawing.Point(0, 0);
            this.tlpTitle.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTitle.Name = "tlpTitle";
            this.tlpTitle.RowCount = 1;
            this.tlpTitle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTitle.Size = new System.Drawing.Size(450, 35);
            this.tlpTitle.TabIndex = 0;
            this.tlpTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tlpTitle_MouseDown);
            this.tlpTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tlpTitle_MouseMove);
            this.tlpTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tlpTitle_MouseUp);
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(3, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(44, 29);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTitle.ForeColor = System.Drawing.Color.Black;
            this.lbTitle.Location = new System.Drawing.Point(53, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(72, 18);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "校准工具";
            // 
            // picCloseWindow
            // 
            this.picCloseWindow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCloseWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCloseWindow.Image = global::HangBalance.Properties.Resources.close;
            this.picCloseWindow.Location = new System.Drawing.Point(415, 10);
            this.picCloseWindow.Margin = new System.Windows.Forms.Padding(10);
            this.picCloseWindow.Name = "picCloseWindow";
            this.picCloseWindow.Size = new System.Drawing.Size(25, 15);
            this.picCloseWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCloseWindow.TabIndex = 3;
            this.picCloseWindow.TabStop = false;
            this.picCloseWindow.Click += new System.EventHandler(this.picCloseWindow_Click);
            // 
            // picSetting
            // 
            this.picSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSetting.Image = global::HangBalance.Properties.Resources.icon_setting;
            this.picSetting.Location = new System.Drawing.Point(368, 8);
            this.picSetting.Margin = new System.Windows.Forms.Padding(8);
            this.picSetting.Name = "picSetting";
            this.picSetting.Size = new System.Drawing.Size(29, 19);
            this.picSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSetting.TabIndex = 4;
            this.picSetting.TabStop = false;
            this.picSetting.Click += new System.EventHandler(this.picSetting_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpTitle, 0, 0);
            this.tlpMain.Controls.Add(this.tlpParameter, 0, 1);
            this.tlpMain.Controls.Add(this.mCalibration, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tlpMain.Size = new System.Drawing.Size(450, 447);
            this.tlpMain.TabIndex = 0;
            // 
            // contextMenu
            // 
            this.contextMenu.BackColor = System.Drawing.SystemColors.HotTrack;
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDebug,
            this.menuZeroCali});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // menuDebug
            // 
            this.menuDebug.AutoSize = false;
            this.menuDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuDebug.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.menuDebug.ForeColor = System.Drawing.Color.White;
            this.menuDebug.Name = "menuDebug";
            this.menuDebug.ShowShortcutKeys = false;
            this.menuDebug.Size = new System.Drawing.Size(155, 22);
            this.menuDebug.Text = "调试模式";
            this.menuDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.menuDebug.Click += new System.EventHandler(this.menuDebug_Click);
            // 
            // menuZeroCali
            // 
            this.menuZeroCali.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuZeroCali.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.menuZeroCali.ForeColor = System.Drawing.Color.White;
            this.menuZeroCali.Name = "menuZeroCali";
            this.menuZeroCali.Size = new System.Drawing.Size(100, 22);
            this.menuZeroCali.Text = "零点标定";
            this.menuZeroCali.Click += new System.EventHandler(this.menuZeroCali_Click);
            // 
            // mCalibration
            // 
            this.mCalibration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mCalibration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(113)))), ((int)(((byte)(185)))));
            this.mCalibration.Location = new System.Drawing.Point(3, 105);
            this.mCalibration.Name = "mCalibration";
            this.mCalibration.Size = new System.Drawing.Size(444, 339);
            this.mCalibration.TabIndex = 2;
            // 
            // PressureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 447);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PressureForm";
            this.Text = "校准工具";
            this.Load += new System.EventHandler(this.PressureForm_Load);
            this.tlpParameter.ResumeLayout(false);
            this.tlpParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectState)).EndInit();
            this.tlpTitle.ResumeLayout(false);
            this.tlpTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCloseWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSetting)).EndInit();
            this.tlpMain.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpParameter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDeviceNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BalanceSerialPort;
        private System.Windows.Forms.TableLayoutPanel tlpTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.PictureBox picCloseWindow;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox picConnectState;
        private Calibration mCalibration;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuDebug;
        private System.Windows.Forms.ToolStripMenuItem menuZeroCali;
        private System.Windows.Forms.PictureBox picSetting;
    }
}