Imports System.Data.SqlClient
Imports System.Reflection.Emit

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Opencon()
        MessageBox.Show("STORE OPENS")
        con.Close()

        cartItems()
        StocksItems()
        resetstock()
        InitializeStock()
        resetcart()
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

    Public Sub cartItems()
        Dim query As String = "SELECT * FROM cart"
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
        Dim query As String = "DELETE FROM cart WHERE total IS NOT NULL AND payment IS NOT NULL AND Change IS NOT NULL"
        Using cmd As SqlCommand = New SqlCommand(query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        TextBox2.Text = ""
        cartItems()
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

    Public Sub resetcart()
        Dim delquery As String = "DELETE FROM cart"
        Using cmd As New SqlCommand(delquery, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()
        End If
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Opencon()

        Dim Item_no As String = TextBox1.Text
        Dim Product_name As String = String.Empty
        Dim StockQuantity As Integer = 0
        Dim Price As Decimal = 0
        Dim cartQuantity As Integer = Convert.ToInt32(NumericUpDown1.Value)

        If cartQuantity <= 0 Then
            MsgBox("Please select a valid quantity.", vbExclamation)
            con.Close()
            Return
        End If

        Dim checkQuery As String = "SELECT Product_name, Quantity, CAST(Price AS DECIMAL(10,2)) AS Price FROM Stocks WHERE Item_no = @Item_no"
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

        If StockQuantity < cartQuantity Then
            MsgBox("Not enough stock available!", vbExclamation)
            con.Close()
            Return
        End If

        Dim existingcartQuantity As Integer = 0
        Dim checkcartQuery As String = "SELECT Quantity FROM cart WHERE Item_No = @Item_no"
        Using cmd As New SqlCommand(checkcartQuery, con)
            cmd.Parameters.AddWithValue("@Item_no", Item_no)
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    existingcartQuantity = Convert.ToInt32(reader("Quantity"))
                End If
            End Using
        End Using

        If existingcartQuantity > 0 Then
            Dim newcartQuantity As Integer = existingcartQuantity + cartQuantity
            Dim updateQuery As String = "UPDATE cart SET Quantity = @Quantity, Price = @Price WHERE Item_No = @Item_no"
            Using updateCmd As New SqlCommand(updateQuery, con)
                updateCmd.Parameters.AddWithValue("@Quantity", newcartQuantity)
                updateCmd.Parameters.AddWithValue("@Price", newcartQuantity * Price)
                updateCmd.Parameters.AddWithValue("@Item_no", Item_no)
                updateCmd.ExecuteNonQuery()
            End Using
        Else
            Dim insertQuery As String = "INSERT INTO cart (Item_No, Product_name, Quantity, Price) VALUES (@Item_no, @Product_name, @Quantity, @Price)"
            Using insertCmd As New SqlCommand(insertQuery, con)
                insertCmd.Parameters.AddWithValue("@Item_no", Item_no)
                insertCmd.Parameters.AddWithValue("@Product_name", Product_name)
                insertCmd.Parameters.AddWithValue("@Quantity", cartQuantity)
                insertCmd.Parameters.AddWithValue("@Price", cartQuantity * Price)
                insertCmd.ExecuteNonQuery()
            End Using
        End If

        Dim updateStockQuery As String = "UPDATE Stocks SET Quantity = Quantity - @cartQuantity WHERE Item_no = @Item_no"
        Using stockCmd As New SqlCommand(updateStockQuery, con)
            stockCmd.Parameters.AddWithValue("@cartQuantity", cartQuantity)
            stockCmd.Parameters.AddWithValue("@Item_no", Item_no)
            stockCmd.ExecuteNonQuery()
        End Using

        MsgBox("Item added to cart successfully.", vbInformation)
        TextBox1.Text = ""
        NumericUpDown1.Value = 1

        cartItems()
        StocksItems()

        Dim totalcartPrice As Decimal = 0
        Dim totalPriceQuery As String = "SELECT SUM(Price) AS TotalPrice FROM cart"
        Using totalPriceCmd As New SqlCommand(totalPriceQuery, con)
            Dim result As Object = totalPriceCmd.ExecuteScalar()
            If result IsNot DBNull.Value Then
                totalcartPrice = Convert.ToDecimal(result)
            End If
        End Using

        TextBox2.Text = totalcartPrice.ToString("F2")

        con.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim TotalcartPrice As Decimal
        Dim PaymentAmount As Decimal

        If Not Decimal.TryParse(TextBox2.Text, TotalcartPrice) Then
            MsgBox("Invalid total price!", vbExclamation)
            Return
        End If

        If Not Decimal.TryParse(TextBox3.Text, PaymentAmount) Then
            MsgBox("Please enter a valid payment amount!", vbExclamation)
            Return
        End If

        If PaymentAmount < TotalcartPrice Then
            MessageBox.Show("Insufficient Payment!")
        Else
            Dim sukli As Decimal = PaymentAmount - TotalcartPrice
            TextBox4.Text = sukli.ToString("F2")
        End If

        Button4.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cartItems()
        StocksItems()


        Dim total As Decimal = Convert.ToDecimal(TextBox2.Text)
        Dim payment As Decimal = Convert.ToDecimal(TextBox3.Text)
        Dim change As Decimal = Convert.ToDecimal(TextBox4.Text)

        Dim totalVAT As Decimal = Decimal.Round(CDec(total * 0.12), 2)

        Dim insertcart As String = "UPDATE cart SET total = @total, payment = @payment, Change = @Change, VAT = @VAT WHERE Item_no IS NOT NULL"
        Using cmd As New SqlCommand(insertcart, con)
            cmd.Parameters.AddWithValue("@total", Decimal.Round(total, 2))
            cmd.Parameters.AddWithValue("@payment", Decimal.Round(payment, 2))
            cmd.Parameters.AddWithValue("@Change", Decimal.Round(change, 2))
            cmd.Parameters.AddWithValue("@VAT", totalVAT)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using
        Form3.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button4.Enabled = False
        resetstock()
        cartItems()
        StocksItems()
        resetcart()
        TextBox3.Text = ""
        TextBox4.Text = ""
        NumericUpDown1.Value = 1
        MsgBox("Cleared succesfully")
    End Sub
End Class
