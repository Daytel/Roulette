namespace Roulette
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.Magnifer = new System.Windows.Forms.Button();
            this.Beer = new System.Windows.Forms.Button();
            this.Adrenalin = new System.Windows.Forms.Button();
            this.Saw = new System.Windows.Forms.Button();
            this.Telephone = new System.Windows.Forms.Button();
            this.Handcuffs = new System.Windows.Forms.Button();
            this.Cigarettes = new System.Windows.Forms.Button();
            this.Inverter = new System.Windows.Forms.Button();
            this.Medicine = new System.Windows.Forms.Button();
            this.BlankButton = new System.Windows.Forms.Button();
            this.LifeRoundButton = new System.Windows.Forms.Button();
            this.LifeRoundCount = new System.Windows.Forms.Label();
            this.BlankCount = new System.Windows.Forms.Label();
            this.LifeRoundMinusButton = new System.Windows.Forms.Button();
            this.BlankMinusButton = new System.Windows.Forms.Button();
            this.You = new System.Windows.Forms.Label();
            this.Dealer = new System.Windows.Forms.Label();
            this.Your_Item = new System.Windows.Forms.ListBox();
            this.Dealer_Item = new System.Windows.Forms.ListBox();
            this.HP2 = new System.Windows.Forms.RadioButton();
            this.HP3 = new System.Windows.Forms.RadioButton();
            this.HP4 = new System.Windows.Forms.RadioButton();
            this.MaxHP = new System.Windows.Forms.GroupBox();
            this.Trash = new System.Windows.Forms.Button();
            this.YH1 = new System.Windows.Forms.RadioButton();
            this.YH2 = new System.Windows.Forms.RadioButton();
            this.YH4 = new System.Windows.Forms.RadioButton();
            this.YH3 = new System.Windows.Forms.RadioButton();
            this.SetHP = new System.Windows.Forms.Label();
            this.YouHp = new System.Windows.Forms.GroupBox();
            this.DealerHp = new System.Windows.Forms.GroupBox();
            this.DH1 = new System.Windows.Forms.RadioButton();
            this.DH2 = new System.Windows.Forms.RadioButton();
            this.DH3 = new System.Windows.Forms.RadioButton();
            this.DH4 = new System.Windows.Forms.RadioButton();
            this.Calculate = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.ListBox();
            this.gameBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MaxHP.SuspendLayout();
            this.YouHp.SuspendLayout();
            this.DealerHp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Magnifer
            // 
            this.Magnifer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Magnifer.BackgroundImage")));
            this.Magnifer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Magnifer.Location = new System.Drawing.Point(224, 14);
            this.Magnifer.Name = "Magnifer";
            this.Magnifer.Size = new System.Drawing.Size(100, 100);
            this.Magnifer.TabIndex = 0;
            this.Magnifer.UseVisualStyleBackColor = true;
            this.Magnifer.Click += new System.EventHandler(this.Item_Click);
            // 
            // Beer
            // 
            this.Beer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Beer.BackgroundImage")));
            this.Beer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Beer.Location = new System.Drawing.Point(12, 224);
            this.Beer.Name = "Beer";
            this.Beer.Size = new System.Drawing.Size(100, 100);
            this.Beer.TabIndex = 1;
            this.Beer.UseVisualStyleBackColor = true;
            this.Beer.Click += new System.EventHandler(this.Item_Click);
            // 
            // Adrenalin
            // 
            this.Adrenalin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Adrenalin.BackgroundImage")));
            this.Adrenalin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Adrenalin.Location = new System.Drawing.Point(12, 12);
            this.Adrenalin.Name = "Adrenalin";
            this.Adrenalin.Size = new System.Drawing.Size(100, 100);
            this.Adrenalin.TabIndex = 2;
            this.Adrenalin.UseVisualStyleBackColor = true;
            this.Adrenalin.Click += new System.EventHandler(this.Item_Click);
            // 
            // Saw
            // 
            this.Saw.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Saw.BackgroundImage")));
            this.Saw.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Saw.Location = new System.Drawing.Point(118, 12);
            this.Saw.Name = "Saw";
            this.Saw.Size = new System.Drawing.Size(100, 100);
            this.Saw.TabIndex = 3;
            this.Saw.UseVisualStyleBackColor = true;
            this.Saw.Click += new System.EventHandler(this.Item_Click);
            // 
            // Telephone
            // 
            this.Telephone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Telephone.BackgroundImage")));
            this.Telephone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Telephone.Location = new System.Drawing.Point(12, 120);
            this.Telephone.Name = "Telephone";
            this.Telephone.Size = new System.Drawing.Size(100, 100);
            this.Telephone.TabIndex = 4;
            this.Telephone.UseVisualStyleBackColor = true;
            this.Telephone.Click += new System.EventHandler(this.Item_Click);
            // 
            // Handcuffs
            // 
            this.Handcuffs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Handcuffs.BackgroundImage")));
            this.Handcuffs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Handcuffs.Location = new System.Drawing.Point(224, 120);
            this.Handcuffs.Name = "Handcuffs";
            this.Handcuffs.Size = new System.Drawing.Size(100, 100);
            this.Handcuffs.TabIndex = 5;
            this.Handcuffs.UseVisualStyleBackColor = true;
            this.Handcuffs.Click += new System.EventHandler(this.Item_Click);
            // 
            // Cigarettes
            // 
            this.Cigarettes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cigarettes.BackgroundImage")));
            this.Cigarettes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cigarettes.Location = new System.Drawing.Point(118, 226);
            this.Cigarettes.Name = "Cigarettes";
            this.Cigarettes.Size = new System.Drawing.Size(100, 100);
            this.Cigarettes.TabIndex = 6;
            this.Cigarettes.UseVisualStyleBackColor = true;
            this.Cigarettes.Click += new System.EventHandler(this.Item_Click);
            // 
            // Inverter
            // 
            this.Inverter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Inverter.BackgroundImage")));
            this.Inverter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Inverter.Location = new System.Drawing.Point(118, 120);
            this.Inverter.Name = "Inverter";
            this.Inverter.Size = new System.Drawing.Size(100, 100);
            this.Inverter.TabIndex = 7;
            this.Inverter.UseVisualStyleBackColor = true;
            this.Inverter.Click += new System.EventHandler(this.Item_Click);
            // 
            // Medicine
            // 
            this.Medicine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Medicine.BackgroundImage")));
            this.Medicine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Medicine.Location = new System.Drawing.Point(224, 226);
            this.Medicine.Name = "Medicine";
            this.Medicine.Size = new System.Drawing.Size(100, 100);
            this.Medicine.TabIndex = 8;
            this.Medicine.UseVisualStyleBackColor = true;
            this.Medicine.Click += new System.EventHandler(this.Item_Click);
            // 
            // BlankButton
            // 
            this.BlankButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BlankButton.BackgroundImage")));
            this.BlankButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BlankButton.Location = new System.Drawing.Point(688, 12);
            this.BlankButton.Name = "BlankButton";
            this.BlankButton.Size = new System.Drawing.Size(100, 100);
            this.BlankButton.TabIndex = 9;
            this.BlankButton.UseVisualStyleBackColor = true;
            this.BlankButton.Click += new System.EventHandler(this.Add_Patron);
            // 
            // LifeRoundButton
            // 
            this.LifeRoundButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LifeRoundButton.BackgroundImage")));
            this.LifeRoundButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LifeRoundButton.Location = new System.Drawing.Point(585, 12);
            this.LifeRoundButton.Name = "LifeRoundButton";
            this.LifeRoundButton.Size = new System.Drawing.Size(100, 100);
            this.LifeRoundButton.TabIndex = 10;
            this.LifeRoundButton.UseVisualStyleBackColor = true;
            this.LifeRoundButton.Click += new System.EventHandler(this.Add_Patron);
            // 
            // LifeRoundCount
            // 
            this.LifeRoundCount.AutoSize = true;
            this.LifeRoundCount.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.LifeRoundCount.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LifeRoundCount.Location = new System.Drawing.Point(584, 120);
            this.LifeRoundCount.Name = "LifeRoundCount";
            this.LifeRoundCount.Size = new System.Drawing.Size(33, 40);
            this.LifeRoundCount.TabIndex = 11;
            this.LifeRoundCount.Text = "0";
            // 
            // BlankCount
            // 
            this.BlankCount.AutoSize = true;
            this.BlankCount.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.BlankCount.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.BlankCount.Location = new System.Drawing.Point(690, 120);
            this.BlankCount.Name = "BlankCount";
            this.BlankCount.Size = new System.Drawing.Size(33, 40);
            this.BlankCount.TabIndex = 12;
            this.BlankCount.Text = "0";
            // 
            // LifeRoundMinusButton
            // 
            this.LifeRoundMinusButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LifeRoundMinusButton.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.LifeRoundMinusButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LifeRoundMinusButton.Location = new System.Drawing.Point(623, 120);
            this.LifeRoundMinusButton.Name = "LifeRoundMinusButton";
            this.LifeRoundMinusButton.Size = new System.Drawing.Size(33, 40);
            this.LifeRoundMinusButton.TabIndex = 13;
            this.LifeRoundMinusButton.Text = "-";
            this.LifeRoundMinusButton.UseVisualStyleBackColor = false;
            this.LifeRoundMinusButton.Click += new System.EventHandler(this.Remove_Patron);
            // 
            // BlankMinusButton
            // 
            this.BlankMinusButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BlankMinusButton.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.BlankMinusButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.BlankMinusButton.Location = new System.Drawing.Point(729, 120);
            this.BlankMinusButton.Name = "BlankMinusButton";
            this.BlankMinusButton.Size = new System.Drawing.Size(33, 40);
            this.BlankMinusButton.TabIndex = 14;
            this.BlankMinusButton.Text = "-";
            this.BlankMinusButton.UseVisualStyleBackColor = false;
            this.BlankMinusButton.Click += new System.EventHandler(this.Remove_Patron);
            // 
            // You
            // 
            this.You.AutoSize = true;
            this.You.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.You.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.You.Location = new System.Drawing.Point(351, 9);
            this.You.Name = "You";
            this.You.Size = new System.Drawing.Size(78, 40);
            this.You.TabIndex = 15;
            this.You.Text = "YOU";
            // 
            // Dealer
            // 
            this.Dealer.AutoSize = true;
            this.Dealer.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.Dealer.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Dealer.Location = new System.Drawing.Point(444, 9);
            this.Dealer.Name = "Dealer";
            this.Dealer.Size = new System.Drawing.Size(132, 40);
            this.Dealer.TabIndex = 16;
            this.Dealer.Text = "DEALER";
            // 
            // Your_Item
            // 
            this.Your_Item.BackColor = System.Drawing.SystemColors.Desktop;
            this.Your_Item.ForeColor = System.Drawing.SystemColors.Window;
            this.Your_Item.FormattingEnabled = true;
            this.Your_Item.ItemHeight = 16;
            this.Your_Item.Location = new System.Drawing.Point(330, 55);
            this.Your_Item.Name = "Your_Item";
            this.Your_Item.Size = new System.Drawing.Size(120, 132);
            this.Your_Item.TabIndex = 22;
            this.Your_Item.Click += new System.EventHandler(this.Change_Array_Item);
            // 
            // Dealer_Item
            // 
            this.Dealer_Item.BackColor = System.Drawing.SystemColors.MenuText;
            this.Dealer_Item.ForeColor = System.Drawing.SystemColors.Window;
            this.Dealer_Item.FormattingEnabled = true;
            this.Dealer_Item.ItemHeight = 16;
            this.Dealer_Item.Location = new System.Drawing.Point(456, 55);
            this.Dealer_Item.Name = "Dealer_Item";
            this.Dealer_Item.Size = new System.Drawing.Size(120, 132);
            this.Dealer_Item.TabIndex = 23;
            this.Dealer_Item.Click += new System.EventHandler(this.Change_Array_Item);
            // 
            // HP2
            // 
            this.HP2.AutoSize = true;
            this.HP2.Checked = true;
            this.HP2.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HP2.ForeColor = System.Drawing.SystemColors.Control;
            this.HP2.Location = new System.Drawing.Point(15, 37);
            this.HP2.Name = "HP2";
            this.HP2.Size = new System.Drawing.Size(43, 31);
            this.HP2.TabIndex = 26;
            this.HP2.TabStop = true;
            this.HP2.Text = "2";
            this.HP2.UseVisualStyleBackColor = true;
            this.HP2.CheckedChanged += new System.EventHandler(this.Change_MAX_HP);
            // 
            // HP3
            // 
            this.HP3.AutoSize = true;
            this.HP3.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HP3.ForeColor = System.Drawing.SystemColors.Control;
            this.HP3.Location = new System.Drawing.Point(84, 37);
            this.HP3.Name = "HP3";
            this.HP3.Size = new System.Drawing.Size(43, 31);
            this.HP3.TabIndex = 27;
            this.HP3.Text = "3";
            this.HP3.UseVisualStyleBackColor = true;
            this.HP3.CheckedChanged += new System.EventHandler(this.Change_MAX_HP);
            // 
            // HP4
            // 
            this.HP4.AutoSize = true;
            this.HP4.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HP4.ForeColor = System.Drawing.SystemColors.Control;
            this.HP4.Location = new System.Drawing.Point(149, 37);
            this.HP4.Name = "HP4";
            this.HP4.Size = new System.Drawing.Size(43, 31);
            this.HP4.TabIndex = 28;
            this.HP4.Text = "4";
            this.HP4.UseVisualStyleBackColor = true;
            this.HP4.CheckedChanged += new System.EventHandler(this.Change_MAX_HP);
            // 
            // MaxHP
            // 
            this.MaxHP.Controls.Add(this.HP2);
            this.MaxHP.Controls.Add(this.HP4);
            this.MaxHP.Controls.Add(this.HP3);
            this.MaxHP.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MaxHP.ForeColor = System.Drawing.SystemColors.Control;
            this.MaxHP.Location = new System.Drawing.Point(588, 163);
            this.MaxHP.Name = "MaxHP";
            this.MaxHP.Size = new System.Drawing.Size(200, 73);
            this.MaxHP.TabIndex = 29;
            this.MaxHP.TabStop = false;
            this.MaxHP.Text = "MAX HP";
            // 
            // Trash
            // 
            this.Trash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Trash.BackgroundImage")));
            this.Trash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Trash.Location = new System.Drawing.Point(516, 188);
            this.Trash.Name = "Trash";
            this.Trash.Size = new System.Drawing.Size(60, 60);
            this.Trash.TabIndex = 30;
            this.Trash.UseVisualStyleBackColor = true;
            this.Trash.Click += new System.EventHandler(this.Array_Clear);
            // 
            // YH1
            // 
            this.YH1.AutoSize = true;
            this.YH1.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YH1.ForeColor = System.Drawing.SystemColors.Control;
            this.YH1.Location = new System.Drawing.Point(6, 11);
            this.YH1.Name = "YH1";
            this.YH1.Size = new System.Drawing.Size(43, 31);
            this.YH1.TabIndex = 29;
            this.YH1.Text = "1";
            this.YH1.UseVisualStyleBackColor = true;
            this.YH1.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // YH2
            // 
            this.YH2.AutoSize = true;
            this.YH2.Checked = true;
            this.YH2.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YH2.ForeColor = System.Drawing.SystemColors.Control;
            this.YH2.Location = new System.Drawing.Point(6, 36);
            this.YH2.Name = "YH2";
            this.YH2.Size = new System.Drawing.Size(43, 31);
            this.YH2.TabIndex = 31;
            this.YH2.TabStop = true;
            this.YH2.Text = "2";
            this.YH2.UseVisualStyleBackColor = true;
            this.YH2.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // YH4
            // 
            this.YH4.AutoSize = true;
            this.YH4.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YH4.ForeColor = System.Drawing.SystemColors.Control;
            this.YH4.Location = new System.Drawing.Point(6, 87);
            this.YH4.Name = "YH4";
            this.YH4.Size = new System.Drawing.Size(43, 31);
            this.YH4.TabIndex = 32;
            this.YH4.Text = "4";
            this.YH4.UseVisualStyleBackColor = true;
            this.YH4.Visible = false;
            this.YH4.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // YH3
            // 
            this.YH3.AutoSize = true;
            this.YH3.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YH3.ForeColor = System.Drawing.SystemColors.Control;
            this.YH3.Location = new System.Drawing.Point(6, 62);
            this.YH3.Name = "YH3";
            this.YH3.Size = new System.Drawing.Size(43, 31);
            this.YH3.TabIndex = 33;
            this.YH3.Text = "3";
            this.YH3.UseVisualStyleBackColor = true;
            this.YH3.Visible = false;
            this.YH3.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // SetHP
            // 
            this.SetHP.AutoSize = true;
            this.SetHP.Font = new System.Drawing.Font("Arial Narrow", 20F, System.Drawing.FontStyle.Bold);
            this.SetHP.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SetHP.Location = new System.Drawing.Point(334, 196);
            this.SetHP.Name = "SetHP";
            this.SetHP.Size = new System.Drawing.Size(56, 40);
            this.SetHP.TabIndex = 34;
            this.SetHP.Text = "HP";
            // 
            // YouHp
            // 
            this.YouHp.Controls.Add(this.YH1);
            this.YouHp.Controls.Add(this.YH2);
            this.YouHp.Controls.Add(this.YH3);
            this.YouHp.Controls.Add(this.YH4);
            this.YouHp.Location = new System.Drawing.Point(396, 188);
            this.YouHp.Name = "YouHp";
            this.YouHp.Size = new System.Drawing.Size(60, 116);
            this.YouHp.TabIndex = 39;
            this.YouHp.TabStop = false;
            this.YouHp.Text = "groupBox1";
            // 
            // DealerHp
            // 
            this.DealerHp.Controls.Add(this.DH1);
            this.DealerHp.Controls.Add(this.DH2);
            this.DealerHp.Controls.Add(this.DH3);
            this.DealerHp.Controls.Add(this.DH4);
            this.DealerHp.Location = new System.Drawing.Point(459, 188);
            this.DealerHp.Name = "DealerHp";
            this.DealerHp.Size = new System.Drawing.Size(61, 116);
            this.DealerHp.TabIndex = 40;
            this.DealerHp.TabStop = false;
            this.DealerHp.Text = "groupBox1";
            // 
            // DH1
            // 
            this.DH1.AutoSize = true;
            this.DH1.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DH1.ForeColor = System.Drawing.SystemColors.Control;
            this.DH1.Location = new System.Drawing.Point(6, 11);
            this.DH1.Name = "DH1";
            this.DH1.Size = new System.Drawing.Size(43, 31);
            this.DH1.TabIndex = 29;
            this.DH1.Text = "1";
            this.DH1.UseVisualStyleBackColor = true;
            this.DH1.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // DH2
            // 
            this.DH2.AutoSize = true;
            this.DH2.Checked = true;
            this.DH2.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DH2.ForeColor = System.Drawing.SystemColors.Control;
            this.DH2.Location = new System.Drawing.Point(6, 36);
            this.DH2.Name = "DH2";
            this.DH2.Size = new System.Drawing.Size(43, 31);
            this.DH2.TabIndex = 31;
            this.DH2.TabStop = true;
            this.DH2.Text = "2";
            this.DH2.UseVisualStyleBackColor = true;
            this.DH2.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // DH3
            // 
            this.DH3.AutoSize = true;
            this.DH3.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DH3.ForeColor = System.Drawing.SystemColors.Control;
            this.DH3.Location = new System.Drawing.Point(6, 62);
            this.DH3.Name = "DH3";
            this.DH3.Size = new System.Drawing.Size(43, 31);
            this.DH3.TabIndex = 33;
            this.DH3.Text = "3";
            this.DH3.UseVisualStyleBackColor = true;
            this.DH3.Visible = false;
            this.DH3.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // DH4
            // 
            this.DH4.AutoSize = true;
            this.DH4.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DH4.ForeColor = System.Drawing.SystemColors.Control;
            this.DH4.Location = new System.Drawing.Point(6, 87);
            this.DH4.Name = "DH4";
            this.DH4.Size = new System.Drawing.Size(43, 31);
            this.DH4.TabIndex = 32;
            this.DH4.Text = "4";
            this.DH4.UseVisualStyleBackColor = true;
            this.DH4.Visible = false;
            this.DH4.CheckedChanged += new System.EventHandler(this.Change_Player_HP);
            // 
            // Calculate
            // 
            this.Calculate.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Calculate.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Calculate.ForeColor = System.Drawing.SystemColors.Control;
            this.Calculate.Location = new System.Drawing.Point(603, 255);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(165, 49);
            this.Calculate.TabIndex = 41;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = false;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // Result
            // 
            this.Result.BackColor = System.Drawing.SystemColors.MenuText;
            this.Result.ForeColor = System.Drawing.SystemColors.Window;
            this.Result.FormattingEnabled = true;
            this.Result.ItemHeight = 16;
            this.Result.Location = new System.Drawing.Point(794, 12);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(235, 132);
            this.Result.TabIndex = 42;
            // 
            // gameBindingSource
            // 
            this.gameBindingSource.DataSource = typeof(Roulette.Game);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1039, 338);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.DealerHp);
            this.Controls.Add(this.YouHp);
            this.Controls.Add(this.SetHP);
            this.Controls.Add(this.Trash);
            this.Controls.Add(this.MaxHP);
            this.Controls.Add(this.Dealer_Item);
            this.Controls.Add(this.Your_Item);
            this.Controls.Add(this.Dealer);
            this.Controls.Add(this.You);
            this.Controls.Add(this.BlankMinusButton);
            this.Controls.Add(this.LifeRoundMinusButton);
            this.Controls.Add(this.BlankCount);
            this.Controls.Add(this.LifeRoundCount);
            this.Controls.Add(this.LifeRoundButton);
            this.Controls.Add(this.BlankButton);
            this.Controls.Add(this.Medicine);
            this.Controls.Add(this.Inverter);
            this.Controls.Add(this.Cigarettes);
            this.Controls.Add(this.Handcuffs);
            this.Controls.Add(this.Telephone);
            this.Controls.Add(this.Saw);
            this.Controls.Add(this.Adrenalin);
            this.Controls.Add(this.Beer);
            this.Controls.Add(this.Magnifer);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Game";
            this.Text = "Roulette Strategy";
            this.MaxHP.ResumeLayout(false);
            this.MaxHP.PerformLayout();
            this.YouHp.ResumeLayout(false);
            this.YouHp.PerformLayout();
            this.DealerHp.ResumeLayout(false);
            this.DealerHp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Magnifer;
        private System.Windows.Forms.Button Beer;
        private System.Windows.Forms.Button Adrenalin;
        private System.Windows.Forms.Button Saw;
        private System.Windows.Forms.Button Telephone;
        private System.Windows.Forms.Button Handcuffs;
        private System.Windows.Forms.Button Cigarettes;
        private System.Windows.Forms.Button Inverter;
        private System.Windows.Forms.Button Medicine;
        private System.Windows.Forms.Button BlankButton;
        private System.Windows.Forms.Button LifeRoundButton;
        private System.Windows.Forms.Label LifeRoundCount;
        private System.Windows.Forms.Label BlankCount;
        private System.Windows.Forms.Button LifeRoundMinusButton;
        private System.Windows.Forms.Button BlankMinusButton;
        private System.Windows.Forms.Label You;
        private System.Windows.Forms.Label Dealer;
        private System.Windows.Forms.BindingSource gameBindingSource;
        private System.Windows.Forms.ListBox Your_Item;
        private System.Windows.Forms.ListBox Dealer_Item;
        private System.Windows.Forms.RadioButton HP2;
        private System.Windows.Forms.RadioButton HP3;
        private System.Windows.Forms.RadioButton HP4;
        private System.Windows.Forms.GroupBox MaxHP;
        private System.Windows.Forms.Button Trash;
        private System.Windows.Forms.RadioButton YH1;
        private System.Windows.Forms.RadioButton YH2;
        private System.Windows.Forms.RadioButton YH4;
        private System.Windows.Forms.RadioButton YH3;
        private System.Windows.Forms.Label SetHP;
        private System.Windows.Forms.GroupBox YouHp;
        private System.Windows.Forms.GroupBox DealerHp;
        private System.Windows.Forms.RadioButton DH1;
        private System.Windows.Forms.RadioButton DH2;
        private System.Windows.Forms.RadioButton DH3;
        private System.Windows.Forms.RadioButton DH4;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.ListBox Result;
    }
}

