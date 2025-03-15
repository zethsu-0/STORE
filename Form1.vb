Imports System.Data.SqlClient

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
        Using cmd As SqlCommand = New SqlCommand(delquery, con)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

        End Using
    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Opencon()
        CheckBox1.Text = "1"
        CheckBox1.Tag = "1"

        CheckBox2.Text = "2"
        CheckBox2.Tag = "2"

        CheckBox3.Text = "3"
        CheckBox3.Tag = "3"

        CheckBox4.Text = "4"
        CheckBox4.Tag = "4"

        CheckBox5.Text = "5"
        CheckBox5.Tag = "5"

        Dim selectedItems As New Dictionary(Of String, Integer)

        For Each ctrl As Control In Me.Controls

            If TypeOf ctrl Is CheckBox Then
                Dim cb As CheckBox = DirectCast(ctrl, CheckBox)
                If cb.Checked Then

                    Dim numCtrlName As String = "NumericUpDown" & cb.Name.Replace("CheckBox", "")
                    Dim numCtrl As NumericUpDown = TryCast(Me.Controls(numCtrlName), NumericUpDown)


                    If numCtrl IsNot Nothing AndAlso numCtrl.Value > 0 Then
                        selectedItems.Add(cb.Tag.ToString(), Convert.ToInt32(numCtrl.Value))
                    End If
                End If
            End If
        Next


        If selectedItems.Count = 0 Then
            MsgBox("Please select at least one item and specify a valid quantity.", vbExclamation)
            con.Close()
            Return
        End If
        If selectedItems.Count > 0 Then
            MsgBox("Items added to cart successfully!", vbInformation)
        End If

        For Each kvp As KeyValuePair(Of String, Integer) In selectedItems
            Dim Item_no As String = kvp.Key
            Dim cartQuantity As Integer = kvp.Value
            Dim Product_name As String = ""
            Dim StockQuantity As Integer = 0
            Dim Price As Decimal = 0


            Dim checkQuery As String = "SELECT Product_name, Quantity, Price FROM Stocks WHERE Item_no = @Item_no"
            Using selectCmd As New SqlCommand(checkQuery, con)
                selectCmd.Parameters.AddWithValue("@Item_no", Item_no)
                Using reader As SqlDataReader = selectCmd.ExecuteReader()
                    reader.Read()
                    Product_name = reader("Product_name").ToString()
                    StockQuantity = Convert.ToInt32(reader("Quantity"))
                    Price = Convert.ToDecimal(reader("Price"))

                End Using
            End Using


            If StockQuantity < cartQuantity Then
                MsgBox("Not enough stock for " & Product_name & "!", vbExclamation)
                Continue For
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


        Next


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

        TextBox2.Text = totalcartPrice.ToString()
        con.Close()


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
        MessageBox.Show("cart cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MessageBox.Show("Kulanggg")
        Else
            Dim sukli As Decimal = PaymentAmount - TotalcartPrice
            TextBox4.Text = sukli



        End If

        Button4.Enabled = True

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso TextBox2.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        If e.KeyChar = "."c AndAlso TextBox2.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cartItems()
        StocksItems()

        Dim total As String = TextBox2.Text
        Dim payment As String = TextBox3.Text
        Dim Change As String = TextBox4.Text


        Dim insertcart As String = "UPDATE cart SET total = @total, payment = @payment, Change = @Change WHERE Item_no IS NOT NULL"

        Using cmd As New SqlCommand(insertcart, con)
            cmd.Parameters.AddWithValue("@total", total)
            cmd.Parameters.AddWithValue("@payment", payment)
            cmd.Parameters.AddWithValue("@Change", Change)


            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()


        End Using
        Form3.Show()

    End Sub
End Class