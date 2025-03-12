Imports System.Data.SqlClient
Imports System.Reflection.Emit

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StocksTableAdapter.Fill(Me.STOREDataSet1.Stocks)
        Opencon()
        MessageBox.Show("STORE OPENS")
        con.Close()


        CartItems()
        StocksItems()
        resetstock()
        InitializeStock()
        resetreceipt()
    End Sub

    Public Sub StocksItems()
        Dim query As String = "SELECT * FROM Stocks"

        Using cmd As SqlCommand = New SqlCommand(query, con)
            Using da As New SqlDataAdapter
                da.SelectCommand = cmd
                Using dt As New DataTable()
                    da.Fill(dt)
                    DataGridView1.DataSource = dt
                End Using
            End Using
        End Using
    End Sub

    Public Sub CartItems()
        Dim query As String = "SELECT * FROM Cart"

        Using cmd As SqlCommand = New SqlCommand(query, con)
            Using da As New SqlDataAdapter
                da.SelectCommand = cmd
                Using dt As New DataTable()
                    da.Fill(dt)
                    DataGridView2.DataSource = dt
                End Using
            End Using
        End Using
    End Sub
    Public Sub resetstock()
        Dim query As String = "DELETE FROM Cart"
        Using cmd As SqlCommand = New SqlCommand(query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        TextBox2.Text = ""
        CartItems()

    End Sub



    Public Sub InitializeStock()
        Opencon()

        Dim stockData As New Dictionary(Of String, Integer) From {
        {"1", 10},
        {"2", 20},
        {"3", 30},
        {"4", 40},
        {"5", 50}
    }

        For Each item In stockData
            Dim query As String = "UPDATE Stocks SET Quantity = @Quantity WHERE Item_no = @Item_no"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Quantity", item.Value)
                cmd.Parameters.AddWithValue("@Item_no", item.Key)
                cmd.ExecuteNonQuery()
            End Using
        Next

        con.Close()
    End Sub

    Public Sub resetreceipt()
        Dim delquery As String = "DELETE FROM receipt"
        Using cmd As SqlCommand = New SqlCommand(delquery, con)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

        End Using
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Opencon()

        Dim Item_no As String = TextBox1.Text
        Dim Product_name As String = String.Empty
        Dim StockQuantity As Integer = 0
        Dim Price As Decimal = 0
        Dim CartQuantity As Integer = Convert.ToInt32(NumericUpDown1.Value) ' Get selected quantity

        ' Check if a valid quantity is selected
        If CartQuantity <= 0 Then
            MsgBox("Please select a valid quantity.", vbExclamation)
            con.Close()
            Return
        End If

        ' Retrieve stock details
        Dim checkQuery As String = "SELECT Product_name, Quantity, Price FROM Stocks WHERE Item_no = @Item_no"
        Using selectCmd As New SqlCommand(checkQuery, con)
            selectCmd.Parameters.AddWithValue("@Item_no", Item_no)
            Using reader As SqlDataReader = selectCmd.ExecuteReader()
                If reader.Read() Then
                    Product_name = reader("Product_name").ToString()
                    StockQuantity = Convert.ToInt32(reader("Quantity"))
                    Price = Convert.ToDecimal(reader("Price"))
                Else
                    MsgBox("Item not found in stocks.", vbCritical)
                    con.Close()
                    Return
                End If
            End Using
        End Using

        ' Check if enough stock is available
        If StockQuantity < CartQuantity Then
            MsgBox("Not enough stock available!", vbExclamation)
            con.Close()
            Return
        End If

        ' Check if item is already in the cart
        Dim existingCartQuantity As Integer = 0
        Dim checkCartQuery As String = "SELECT Quantity FROM Cart WHERE Item_No = @Item_no"
        Using cmd As New SqlCommand(checkCartQuery, con)
            cmd.Parameters.AddWithValue("@Item_no", Item_no)
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    existingCartQuantity = Convert.ToInt32(reader("Quantity"))
                End If
            End Using
        End Using


        If existingCartQuantity > 0 Then

            Dim newCartQuantity As Integer = existingCartQuantity + CartQuantity
            Dim updateQuery As String = "UPDATE Cart SET Quantity = @Quantity, Price = @Price WHERE Item_No = @Item_no"
            Using updateCmd As New SqlCommand(updateQuery, con)
                updateCmd.Parameters.AddWithValue("@Quantity", newCartQuantity)
                updateCmd.Parameters.AddWithValue("@Price", newCartQuantity * Price)
                updateCmd.Parameters.AddWithValue("@Item_no", Item_no)
                updateCmd.ExecuteNonQuery()
            End Using
        Else

            Dim insertQuery As String = "INSERT INTO Cart (Item_No, Product_name, Quantity, Price) VALUES (@Item_no, @Product_name, @Quantity, @Price)"
            Using insertCmd As New SqlCommand(insertQuery, con)
                insertCmd.Parameters.AddWithValue("@Item_no", Item_no)
                insertCmd.Parameters.AddWithValue("@Product_name", Product_name)
                insertCmd.Parameters.AddWithValue("@Quantity", CartQuantity)
                insertCmd.Parameters.AddWithValue("@Price", CartQuantity * Price)
                insertCmd.ExecuteNonQuery()
            End Using
        End If


        Dim updateStockQuery As String = "UPDATE Stocks SET Quantity = Quantity - @CartQuantity WHERE Item_no = @Item_no"
        Using stockCmd As New SqlCommand(updateStockQuery, con)
            stockCmd.Parameters.AddWithValue("@CartQuantity", CartQuantity)
            stockCmd.Parameters.AddWithValue("@Item_no", Item_no)
            stockCmd.ExecuteNonQuery()
        End Using

        MsgBox("Item added to Cart successfully.", vbInformation)
        TextBox1.Text = ""
        NumericUpDown1.Value = 1


        CartItems()
        StocksItems()


        Dim totalCartPrice As Decimal = 0
        Dim totalPriceQuery As String = "SELECT SUM(Price) AS TotalPrice FROM Cart"
        Using totalPriceCmd As New SqlCommand(totalPriceQuery, con)
            Dim result As Object = totalPriceCmd.ExecuteScalar()
            If result IsNot DBNull.Value Then
                totalCartPrice = Convert.ToDecimal(result)
            End If
        End Using

        TextBox2.Text = totalCartPrice.ToString()
        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        resetstock()
        CartItems()
        StocksItems()
        resetreceipt()
        TextBox3.Text = ""
        TextBox4.Text = ""
        NumericUpDown1.Value = 1
        MessageBox.Show("Cart cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim TotalCartPrice As Decimal
        Dim PaymentAmount As Decimal

        If Not Decimal.TryParse(TextBox2.Text, TotalCartPrice) Then
            MsgBox("Invalid total price!", vbExclamation)
            Return
        End If

        If Not Decimal.TryParse(TextBox3.Text, PaymentAmount) Then
            MsgBox("Please enter a valid payment amount!", vbExclamation)
            Return
        End If


        If PaymentAmount < TotalCartPrice Then
            MessageBox.Show("Kulanggg")
        Else
            Dim Change As Decimal = PaymentAmount - TotalCartPrice
            TextBox4.Text = Change



        End If

        resetreceipt()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso TextBox2.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso TextBox2.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CartItems()
        StocksItems()

        Dim TotalCartAmount As String = TextBox2.Text
        Dim AmountEntered As String = TextBox3.Text
        Dim Change As String = TextBox4.Text


        Dim insertreceipt As String = "INSERT INTO receipt (TotalCartAmount, AmountEntered, Change) VALUES (@TotalCartAmount, @AmountEntered, @Change)"

        Using cmd As New SqlCommand(insertreceipt, con)
            cmd.Parameters.AddWithValue("@TotalCartAmount", TotalCartAmount)
            cmd.Parameters.AddWithValue("@AmountEntered", AmountEntered)
            cmd.Parameters.AddWithValue("@Change", Change)


            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()



            Form3.Show()


        End Using
    End Sub
End Class