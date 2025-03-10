<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ItemnoDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProductnameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QuantityDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PriceDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StocksBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.STOREDataSet1 = New STORE.STOREDataSet1()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.STOREDataSet = New STORE.STOREDataSet()
        Me.STOREDataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StocksTableAdapter = New STORE.STOREDataSet1TableAdapters.StocksTableAdapter()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.STOREDataSet2 = New STORE.STOREDataSet2()
        Me.CartBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CartTableAdapter = New STORE.STOREDataSet2TableAdapters.CartTableAdapter()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StocksBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.STOREDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.STOREDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.STOREDataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.STOREDataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CartBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ItemnoDataGridViewTextBoxColumn, Me.ProductnameDataGridViewTextBoxColumn, Me.QuantityDataGridViewTextBoxColumn, Me.PriceDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.StocksBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(80, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(631, 161)
        Me.DataGridView1.TabIndex = 0
        '
        'ItemnoDataGridViewTextBoxColumn
        '
        Me.ItemnoDataGridViewTextBoxColumn.DataPropertyName = "Item_no"
        Me.ItemnoDataGridViewTextBoxColumn.HeaderText = "Item_no"
        Me.ItemnoDataGridViewTextBoxColumn.Name = "ItemnoDataGridViewTextBoxColumn"
        '
        'ProductnameDataGridViewTextBoxColumn
        '
        Me.ProductnameDataGridViewTextBoxColumn.DataPropertyName = "Product_name"
        Me.ProductnameDataGridViewTextBoxColumn.HeaderText = "Product_name"
        Me.ProductnameDataGridViewTextBoxColumn.Name = "ProductnameDataGridViewTextBoxColumn"
        '
        'QuantityDataGridViewTextBoxColumn
        '
        Me.QuantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity"
        Me.QuantityDataGridViewTextBoxColumn.HeaderText = "Quantity"
        Me.QuantityDataGridViewTextBoxColumn.Name = "QuantityDataGridViewTextBoxColumn"
        '
        'PriceDataGridViewTextBoxColumn
        '
        Me.PriceDataGridViewTextBoxColumn.DataPropertyName = "Price"
        Me.PriceDataGridViewTextBoxColumn.HeaderText = "Price"
        Me.PriceDataGridViewTextBoxColumn.Name = "PriceDataGridViewTextBoxColumn"
        '
        'StocksBindingSource
        '
        Me.StocksBindingSource.DataMember = "Stocks"
        Me.StocksBindingSource.DataSource = Me.STOREDataSet1
        '
        'STOREDataSet1
        '
        Me.STOREDataSet1.DataSetName = "STOREDataSet1"
        Me.STOREDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'STOREDataSet
        '
        Me.STOREDataSet.DataSetName = "STOREDataSet"
        Me.STOREDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'STOREDataSetBindingSource
        '
        Me.STOREDataSetBindingSource.DataSource = Me.STOREDataSet
        Me.STOREDataSetBindingSource.Position = 0
        '
        'StocksTableAdapter
        '
        Me.StocksTableAdapter.ClearBeforeFill = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(215, 191)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(160, 56)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Add to cart"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(398, 210)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(200, 20)
        Me.TextBox1.TabIndex = 2
        '
        'DataGridView2
        '
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.DataGridView2.DataSource = Me.CartBindingSource
        Me.DataGridView2.Location = New System.Drawing.Point(80, 253)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(631, 139)
        Me.DataGridView2.TabIndex = 3
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Item_no"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Item_no"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Product_name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Product_name"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Quantity"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Quantity"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Price"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Price"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'STOREDataSet2
        '
        Me.STOREDataSet2.DataSetName = "STOREDataSet2"
        Me.STOREDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CartBindingSource
        '
        Me.CartBindingSource.DataMember = "Cart"
        Me.CartBindingSource.DataSource = Me.STOREDataSet2
        '
        'CartTableAdapter
        '
        Me.CartTableAdapter.ClearBeforeFill = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(305, 409)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(158, 20)
        Me.TextBox2.TabIndex = 4
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(305, 452)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(158, 20)
        Me.TextBox3.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(96, 407)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Total"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(96, 459)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Amount"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(303, 494)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(160, 30)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Proceed"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(305, 546)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(158, 20)
        Me.TextBox4.TabIndex = 9
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(303, 581)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(160, 30)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "New"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(96, 553)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Change"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 614)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StocksBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.STOREDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.STOREDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.STOREDataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.STOREDataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CartBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents STOREDataSetBindingSource As BindingSource
    Friend WithEvents STOREDataSet As STOREDataSet
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents STOREDataSet1 As STOREDataSet1
    Friend WithEvents StocksBindingSource As BindingSource
    Friend WithEvents StocksTableAdapter As STOREDataSet1TableAdapters.StocksTableAdapter
    Friend WithEvents ItemnoDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ProductnameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents QuantityDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PriceDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents STOREDataSet2 As STOREDataSet2
    Friend WithEvents CartBindingSource As BindingSource
    Friend WithEvents CartTableAdapter As STOREDataSet2TableAdapters.CartTableAdapter
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
End Class
