
namespace RevitAddIn
{
    partial class SelectArchitecturalFloorsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listBox_AllTypes = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Button_AddTypes = new System.Windows.Forms.Button();
            this.Button_AddAll = new System.Windows.Forms.Button();
            this.Button_RemoveSelected = new System.Windows.Forms.Button();
            this.Button_RemoveAll = new System.Windows.Forms.Button();
            this.Button_Calculate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBox_ScenarioMode = new System.Windows.Forms.ComboBox();
            this.listBox_SelectedTypes = new System.Windows.Forms.ListBox();
            this.Button_SelectType = new System.Windows.Forms.Button();
            this.Button_PairwiseComparison = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView_Result = new System.Windows.Forms.DataGridView();
            this.Rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FloorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmbodiedEnergy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SocialScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalTopsisScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Result)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_AllTypes
            // 
            this.listBox_AllTypes.FormattingEnabled = true;
            this.listBox_AllTypes.Location = new System.Drawing.Point(32, 37);
            this.listBox_AllTypes.Name = "listBox_AllTypes";
            this.listBox_AllTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_AllTypes.Size = new System.Drawing.Size(307, 212);
            this.listBox_AllTypes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All Available Types";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(589, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selected Types";
            // 
            // Button_AddTypes
            // 
            this.Button_AddTypes.Location = new System.Drawing.Point(416, 37);
            this.Button_AddTypes.Name = "Button_AddTypes";
            this.Button_AddTypes.Size = new System.Drawing.Size(103, 23);
            this.Button_AddTypes.TabIndex = 2;
            this.Button_AddTypes.Text = "Add Selected";
            this.Button_AddTypes.UseVisualStyleBackColor = true;
            this.Button_AddTypes.Click += new System.EventHandler(this.Button_AddTypes_Click);
            // 
            // Button_AddAll
            // 
            this.Button_AddAll.Location = new System.Drawing.Point(416, 66);
            this.Button_AddAll.Name = "Button_AddAll";
            this.Button_AddAll.Size = new System.Drawing.Size(103, 23);
            this.Button_AddAll.TabIndex = 2;
            this.Button_AddAll.Text = "Add All";
            this.Button_AddAll.UseVisualStyleBackColor = true;
            this.Button_AddAll.Click += new System.EventHandler(this.Button_AddAll_Click);
            // 
            // Button_RemoveSelected
            // 
            this.Button_RemoveSelected.Location = new System.Drawing.Point(416, 112);
            this.Button_RemoveSelected.Name = "Button_RemoveSelected";
            this.Button_RemoveSelected.Size = new System.Drawing.Size(103, 23);
            this.Button_RemoveSelected.TabIndex = 2;
            this.Button_RemoveSelected.Text = "Remove Selected";
            this.Button_RemoveSelected.UseVisualStyleBackColor = true;
            this.Button_RemoveSelected.Click += new System.EventHandler(this.Button_RemoveSelected_Click);
            // 
            // Button_RemoveAll
            // 
            this.Button_RemoveAll.Location = new System.Drawing.Point(416, 141);
            this.Button_RemoveAll.Name = "Button_RemoveAll";
            this.Button_RemoveAll.Size = new System.Drawing.Size(103, 23);
            this.Button_RemoveAll.TabIndex = 2;
            this.Button_RemoveAll.Text = "Remove All";
            this.Button_RemoveAll.UseVisualStyleBackColor = true;
            this.Button_RemoveAll.Click += new System.EventHandler(this.Button_RemoveAll_Click);
            // 
            // Button_Calculate
            // 
            this.Button_Calculate.Location = new System.Drawing.Point(32, 323);
            this.Button_Calculate.Name = "Button_Calculate";
            this.Button_Calculate.Size = new System.Drawing.Size(75, 72);
            this.Button_Calculate.TabIndex = 3;
            this.Button_Calculate.Text = "Calculate";
            this.Button_Calculate.UseVisualStyleBackColor = true;
            this.Button_Calculate.Click += new System.EventHandler(this.Button_Calculate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(427, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select Scenario";
            // 
            // ComboBox_ScenarioMode
            // 
            this.ComboBox_ScenarioMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_ScenarioMode.FormattingEnabled = true;
            this.ComboBox_ScenarioMode.Location = new System.Drawing.Point(416, 221);
            this.ComboBox_ScenarioMode.Name = "ComboBox_ScenarioMode";
            this.ComboBox_ScenarioMode.Size = new System.Drawing.Size(103, 21);
            this.ComboBox_ScenarioMode.TabIndex = 4;
            this.ComboBox_ScenarioMode.SelectedIndexChanged += new System.EventHandler(this.ComboBox_ScenarioMode_SelectedIndexChanged);
            // 
            // listBox_SelectedTypes
            // 
            this.listBox_SelectedTypes.FormattingEnabled = true;
            this.listBox_SelectedTypes.Location = new System.Drawing.Point(592, 37);
            this.listBox_SelectedTypes.Name = "listBox_SelectedTypes";
            this.listBox_SelectedTypes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox_SelectedTypes.Size = new System.Drawing.Size(307, 212);
            this.listBox_SelectedTypes.TabIndex = 0;
            // 
            // Button_SelectType
            // 
            this.Button_SelectType.Location = new System.Drawing.Point(32, 517);
            this.Button_SelectType.Name = "Button_SelectType";
            this.Button_SelectType.Size = new System.Drawing.Size(75, 71);
            this.Button_SelectType.TabIndex = 5;
            this.Button_SelectType.Text = "Choose Selected Type";
            this.Button_SelectType.UseVisualStyleBackColor = true;
            this.Button_SelectType.Click += new System.EventHandler(this.Button_SelectType_Click);
            // 
            // Button_PairwiseComparison
            // 
            this.Button_PairwiseComparison.Enabled = false;
            this.Button_PairwiseComparison.Location = new System.Drawing.Point(374, 273);
            this.Button_PairwiseComparison.Name = "Button_PairwiseComparison";
            this.Button_PairwiseComparison.Size = new System.Drawing.Size(186, 23);
            this.Button_PairwiseComparison.TabIndex = 2;
            this.Button_PairwiseComparison.Text = "Enter Pairwise Comparison Form";
            this.Button_PairwiseComparison.UseVisualStyleBackColor = true;
            this.Button_PairwiseComparison.Click += new System.EventHandler(this.Button_PairwiseComparison_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(406, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Use Pairwise Comparison";
            // 
            // dataGridView_Result
            // 
            this.dataGridView_Result.AllowUserToAddRows = false;
            this.dataGridView_Result.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Result.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Result.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rank,
            this.FloorType,
            this.Cost,
            this.EmbodiedEnergy,
            this.SocialScore,
            this.FinalTopsisScore});
            this.dataGridView_Result.Location = new System.Drawing.Point(113, 323);
            this.dataGridView_Result.MultiSelect = false;
            this.dataGridView_Result.Name = "dataGridView_Result";
            this.dataGridView_Result.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Result.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Result.Size = new System.Drawing.Size(786, 265);
            this.dataGridView_Result.TabIndex = 6;
            // 
            // Rank
            // 
            this.Rank.HeaderText = "Rank";
            this.Rank.Name = "Rank";
            this.Rank.ReadOnly = true;
            this.Rank.Width = 40;
            // 
            // FloorType
            // 
            this.FloorType.HeaderText = "Floor Type";
            this.FloorType.Name = "FloorType";
            this.FloorType.ReadOnly = true;
            this.FloorType.Width = 300;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "Cost";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            // 
            // EmbodiedEnergy
            // 
            this.EmbodiedEnergy.HeaderText = "Embodied Energy";
            this.EmbodiedEnergy.Name = "EmbodiedEnergy";
            this.EmbodiedEnergy.ReadOnly = true;
            // 
            // SocialScore
            // 
            this.SocialScore.HeaderText = "Social Score";
            this.SocialScore.Name = "SocialScore";
            this.SocialScore.ReadOnly = true;
            // 
            // FinalTopsisScore
            // 
            this.FinalTopsisScore.HeaderText = "Final Topsis Score";
            this.FinalTopsisScore.Name = "FinalTopsisScore";
            this.FinalTopsisScore.ReadOnly = true;
            // 
            // SelectArchitecturalFloorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 632);
            this.Controls.Add(this.dataGridView_Result);
            this.Controls.Add(this.Button_SelectType);
            this.Controls.Add(this.ComboBox_ScenarioMode);
            this.Controls.Add(this.Button_Calculate);
            this.Controls.Add(this.Button_PairwiseComparison);
            this.Controls.Add(this.Button_RemoveAll);
            this.Controls.Add(this.Button_RemoveSelected);
            this.Controls.Add(this.Button_AddAll);
            this.Controls.Add(this.Button_AddTypes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_SelectedTypes);
            this.Controls.Add(this.listBox_AllTypes);
            this.Name = "SelectArchitecturalFloorsForm";
            this.Text = "Select Architectural Floors Form";
            this.Load += new System.EventHandler(this.SelectArchitecturalFloorsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_AllTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Button_AddTypes;
        private System.Windows.Forms.Button Button_AddAll;
        private System.Windows.Forms.Button Button_RemoveSelected;
        private System.Windows.Forms.Button Button_RemoveAll;
        private System.Windows.Forms.Button Button_Calculate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBox_ScenarioMode;
        private System.Windows.Forms.ListBox listBox_SelectedTypes;
        private System.Windows.Forms.Button Button_SelectType;
        private System.Windows.Forms.Button Button_PairwiseComparison;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView_Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rank;
        private System.Windows.Forms.DataGridViewTextBoxColumn FloorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmbodiedEnergy;
        private System.Windows.Forms.DataGridViewTextBoxColumn SocialScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinalTopsisScore;
    }
}