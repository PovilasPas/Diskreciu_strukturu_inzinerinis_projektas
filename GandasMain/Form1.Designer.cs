
namespace GandasMain
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
            this.GView = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            this.Read = new System.Windows.Forms.Button();
            this.SVertices = new System.Windows.Forms.ListBox();
            this.Execute = new System.Windows.Forms.Button();
            this.Previous = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GView
            // 
            this.GView.ArrowheadLength = 10D;
            this.GView.AsyncLayout = false;
            this.GView.AutoScroll = true;
            this.GView.BackwardEnabled = false;
            this.GView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GView.BuildHitTree = true;
            this.GView.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.MDS;
            this.GView.Dock = System.Windows.Forms.DockStyle.Left;
            this.GView.EdgeInsertButtonVisible = false;
            this.GView.FileName = "";
            this.GView.ForwardEnabled = false;
            this.GView.Graph = null;
            this.GView.InsertingEdge = false;
            this.GView.LayoutAlgorithmSettingsButtonVisible = false;
            this.GView.LayoutEditingEnabled = false;
            this.GView.Location = new System.Drawing.Point(0, 0);
            this.GView.LooseOffsetForRouting = 0.25D;
            this.GView.MouseHitDistance = 0.05D;
            this.GView.Name = "GView";
            this.GView.NavigationVisible = false;
            this.GView.NeedToCalculateLayout = true;
            this.GView.OffsetForRelaxingInRouting = 0.6D;
            this.GView.PaddingForEdgeRouting = 8D;
            this.GView.PanButtonPressed = true;
            this.GView.SaveAsImageEnabled = false;
            this.GView.SaveAsMsaglEnabled = false;
            this.GView.SaveButtonVisible = false;
            this.GView.SaveGraphButtonVisible = false;
            this.GView.SaveInVectorFormatEnabled = false;
            this.GView.Size = new System.Drawing.Size(1000, 953);
            this.GView.TabIndex = 0;
            this.GView.TightOffsetForRouting = 0.125D;
            this.GView.ToolBarIsVisible = false;
            this.GView.Transform = ((Microsoft.Msagl.Core.Geometry.Curves.PlaneTransformation)(resources.GetObject("GView.Transform")));
            this.GView.UndoRedoButtonsVisible = true;
            this.GView.WindowZoomButtonPressed = false;
            this.GView.ZoomF = 1D;
            this.GView.ZoomWindowThreshold = 0.05D;
            // 
            // Read
            // 
            this.Read.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Read.Location = new System.Drawing.Point(1006, 12);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(464, 55);
            this.Read.TabIndex = 1;
            this.Read.Text = "Nuskaityti duomenis iš failo";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // SVertices
            // 
            this.SVertices.Enabled = false;
            this.SVertices.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.SVertices.FormattingEnabled = true;
            this.SVertices.ItemHeight = 21;
            this.SVertices.Location = new System.Drawing.Point(1006, 87);
            this.SVertices.Name = "SVertices";
            this.SVertices.Size = new System.Drawing.Size(229, 277);
            this.SVertices.TabIndex = 2;
            this.SVertices.SelectedIndexChanged += new System.EventHandler(this.SVertices_SelectedIndexChanged);
            // 
            // Execute
            // 
            this.Execute.Enabled = false;
            this.Execute.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Execute.Location = new System.Drawing.Point(1241, 87);
            this.Execute.Name = "Execute";
            this.Execute.Size = new System.Drawing.Size(229, 46);
            this.Execute.TabIndex = 3;
            this.Execute.Text = "Skleisti gandą";
            this.Execute.UseVisualStyleBackColor = true;
            this.Execute.Click += new System.EventHandler(this.Execute_Click);
            // 
            // Previous
            // 
            this.Previous.Enabled = false;
            this.Previous.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Previous.Location = new System.Drawing.Point(1006, 895);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(229, 46);
            this.Previous.TabIndex = 4;
            this.Previous.Text = "Atgal";
            this.Previous.UseVisualStyleBackColor = true;
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // Next
            // 
            this.Next.Enabled = false;
            this.Next.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Next.Location = new System.Drawing.Point(1241, 895);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(229, 46);
            this.Next.TabIndex = 5;
            this.Next.Text = "Pimyn";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1482, 953);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.Execute);
            this.Controls.Add(this.SVertices);
            this.Controls.Add(this.Read);
            this.Controls.Add(this.GView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Msagl.GraphViewerGdi.GViewer GView;
        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.ListBox SVertices;
        private System.Windows.Forms.Button Execute;
        private System.Windows.Forms.Button Previous;
        private System.Windows.Forms.Button Next;
    }
}

