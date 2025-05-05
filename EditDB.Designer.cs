namespace OmerEisCommon {
	partial class EditDB {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			btnOK = new Button();
			btnCancel = new Button();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			txtbxServer = new TextBox();
			txtbxDatabase = new TextBox();
			txtbxUsername = new TextBox();
			txtbxPassword = new TextBox();
			listBox1 = new ListBox();
			label5 = new Label();
			txtbxTitle = new TextBox();
			btnTest = new Button();
			txtJson = new TextBox();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			comboDatabases = new ComboBox();
			btnSqlServerDefaults = new Button();
			label6 = new Label();
			txtConnection = new TextBox();
			SuspendLayout();
			// 
			// btnOK
			// 
			btnOK.ImageAlign = ContentAlignment.MiddleLeft;
			btnOK.Location = new Point(112, 295);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(75, 23);
			btnOK.TabIndex = 6;
			btnOK.Text = "OK";
			btnOK.UseVisualStyleBackColor = true;
			btnOK.Click += btnOK_Click;
			// 
			// btnCancel
			// 
			btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
			btnCancel.Location = new Point(224, 295);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 7;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(87, 63);
			label1.Name = "label1";
			label1.Size = new Size(39, 15);
			label1.TabIndex = 2;
			label1.Text = "Server";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(71, 96);
			label2.Name = "label2";
			label2.Size = new Size(55, 15);
			label2.TabIndex = 3;
			label2.Text = "Database";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(61, 138);
			label3.Name = "label3";
			label3.Size = new Size(65, 15);
			label3.TabIndex = 4;
			label3.Text = "User Name";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(69, 175);
			label4.Name = "label4";
			label4.Size = new Size(57, 15);
			label4.TabIndex = 5;
			label4.Text = "Password";
			// 
			// txtbxServer
			// 
			txtbxServer.Location = new Point(147, 60);
			txtbxServer.Name = "txtbxServer";
			txtbxServer.Size = new Size(100, 23);
			txtbxServer.TabIndex = 1;
			// 
			// txtbxDatabase
			// 
			txtbxDatabase.Location = new Point(418, 194);
			txtbxDatabase.Name = "txtbxDatabase";
			txtbxDatabase.Size = new Size(100, 23);
			txtbxDatabase.TabIndex = 2;
			// 
			// txtbxUsername
			// 
			txtbxUsername.Location = new Point(147, 130);
			txtbxUsername.Name = "txtbxUsername";
			txtbxUsername.Size = new Size(100, 23);
			txtbxUsername.TabIndex = 3;
			// 
			// txtbxPassword
			// 
			txtbxPassword.Location = new Point(147, 167);
			txtbxPassword.Name = "txtbxPassword";
			txtbxPassword.Size = new Size(100, 23);
			txtbxPassword.TabIndex = 4;
			// 
			// listBox1
			// 
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 15;
			listBox1.Location = new Point(32, 41);
			listBox1.Name = "listBox1";
			listBox1.Size = new Size(33, 94);
			listBox1.TabIndex = 10;
			listBox1.Visible = false;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(71, 24);
			label5.Name = "label5";
			label5.Size = new Size(29, 15);
			label5.TabIndex = 11;
			label5.Text = "Title";
			label5.Visible = false;
			// 
			// txtbxTitle
			// 
			txtbxTitle.Location = new Point(147, 21);
			txtbxTitle.Name = "txtbxTitle";
			txtbxTitle.Size = new Size(100, 23);
			txtbxTitle.TabIndex = 0;
			txtbxTitle.Visible = false;
			// 
			// btnTest
			// 
			btnTest.Location = new Point(275, 123);
			btnTest.Name = "btnTest";
			btnTest.Size = new Size(75, 23);
			btnTest.TabIndex = 12;
			btnTest.Text = "Test...";
			btnTest.UseVisualStyleBackColor = true;
			btnTest.Click += btnTest_Click;
			// 
			// txtJson
			// 
			txtJson.Location = new Point(405, 14);
			txtJson.Multiline = true;
			txtJson.Name = "txtJson";
			txtJson.Size = new Size(214, 165);
			txtJson.TabIndex = 13;
			// 
			// button1
			// 
			button1.Location = new Point(266, 72);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 14;
			button1.Text = "Compose";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(266, 14);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 15;
			button2.Text = "Clear";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(266, 43);
			button3.Name = "button3";
			button3.Size = new Size(75, 23);
			button3.TabIndex = 16;
			button3.Text = "Json";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// comboDatabases
			// 
			comboDatabases.DropDownStyle = ComboBoxStyle.DropDownList;
			comboDatabases.FormattingEnabled = true;
			comboDatabases.Location = new Point(147, 96);
			comboDatabases.Name = "comboDatabases";
			comboDatabases.Size = new Size(100, 23);
			comboDatabases.TabIndex = 17;
			// 
			// btnSqlServerDefaults
			// 
			btnSqlServerDefaults.Location = new Point(275, 167);
			btnSqlServerDefaults.Name = "btnSqlServerDefaults";
			btnSqlServerDefaults.Size = new Size(75, 23);
			btnSqlServerDefaults.TabIndex = 18;
			btnSqlServerDefaults.Text = "Defaults";
			btnSqlServerDefaults.UseVisualStyleBackColor = true;
			btnSqlServerDefaults.Click += btnSqlServerDefaults_Click;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(64, 228);
			label6.Name = "label6";
			label6.Size = new Size(103, 15);
			label6.TabIndex = 19;
			label6.Text = "Connection String";
			// 
			// txtConnection
			// 
			txtConnection.Location = new Point(183, 228);
			txtConnection.Name = "txtConnection";
			txtConnection.Size = new Size(436, 23);
			txtConnection.TabIndex = 20;
			// 
			// EditDB
			// 
			AcceptButton = btnOK;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancel;
			ClientSize = new Size(645, 405);
			Controls.Add(txtConnection);
			Controls.Add(label6);
			Controls.Add(btnSqlServerDefaults);
			Controls.Add(comboDatabases);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(txtJson);
			Controls.Add(btnTest);
			Controls.Add(txtbxTitle);
			Controls.Add(label5);
			Controls.Add(txtbxPassword);
			Controls.Add(txtbxUsername);
			Controls.Add(txtbxDatabase);
			Controls.Add(txtbxServer);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(btnCancel);
			Controls.Add(btnOK);
			Controls.Add(listBox1);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "EditDB";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Database Parameters";
			Load += EditDB_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnOK;
		private Button btnCancel;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private TextBox txtbxServer;
		private TextBox txtbxDatabase;
		private TextBox txtbxUsername;
		private TextBox txtbxPassword;
		private ListBox listBox1;
		private Label label5;
		private TextBox txtbxTitle;
		private Button btnTest;
		private TextBox txtJson;
		private Button button1;
		private Button button2;
		private Button button3;
		private ComboBox comboDatabases;
		private Button btnSqlServerDefaults;
		private Label label6;
		private TextBox txtConnection;
	}
}