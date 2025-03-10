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



    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Opencon()

        Dim Item_no As String = TextBox1.Text
        Dim Product_name As String = String.Empty
        Dim Quantity As Integer = 0
        Dim Price As Decimal = 0


        Dim checkQuery As String = "SELECT Product_name, Quantity, Price FROM stocks WHERE Item_no = @Item_no"
        Using Cmd As New SqlCommand(checkQuery, con)
            Cmd.Parameters.AddWithValue("@Item_no", Item_no)

            Using reader As SqlDataReader = Cmd.ExecuteReader()
                If reader.Read() Then
                    Product_name = reader("Product_name")
                    Quantity = reader("Quantity")
                    Price = reader("Price")

                Else
                    MsgBox("Item not found in stocks.", vbCritical)
                    con.Close()
                    Return
                End If
            End Using
        End Using


        If Quantity <= 0 Then
            MsgBox("Out of stock!", vbExclamation)
            con.Close()
            Return
        End If


        Dim cartQuantity As Integer = 0
        Dim checkCartQuery As String = "SELECT Quantity FROM Cart WHERE Item_No = @Item_no"
        Using cmd As New SqlCommand(checkCartQuery, con)
            cmd.Parameters.AddWithValue("@Item_no", Item_no)
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    cartQuantity = Convert.ToInt32(reader("Quantity")) + 1
                End If
            End Using
        End Using

        If cartQuantity > 0 Then
            Dim updateQuery As String = "UPDATE Cart SET Quantity = @Quantity, Price = @Price WHERE Item_No = @Item_no"
            Using updateCmd As New SqlCommand(updateQuery, con)
                updateCmd.Parameters.AddWithValue("@Quantity", cartQuantity)
                updateCmd.Parameters.AddWithValue("@Price", cartQuantity * Price)
                updateCmd.Parameters.AddWithValue("@Item_no", Item_no)
                updateCmd.ExecuteNonQuery()
            End Using
        Else
            Dim insertQuery As String = "INSERT INTO Cart (Item_No, Product_name, Quantity, Price) VALUES (@Item_no, @Product_name, @Quantity, @Price)"
            Using insertCmd As New SqlCommand(insertQuery, con)
                insertCmd.Parameters.AddWithValue("@Item_no", Item_no)
                insertCmd.Parameters.AddWithValue("@Product_name", Product_name)
                insertCmd.Parameters.AddWithValue("@Quantity", 1)
                insertCmd.Parameters.AddWithValue("@Price", Price)
                insertCmd.ExecuteNonQuery()
            End Using
        End If

        Dim updateStockQuery As String = "UPDATE Stocks SET Quantity = Quantity - 1 WHERE Item_no = @Item_no"
        Using stockCmd As New SqlCommand(updateStockQuery, con)
            stockCmd.Parameters.AddWithValue("@Item_no", Item_no)
            stockCmd.ExecuteNonQuery()
        End Using

        MsgBox("Item added to Cart successfully.", vbInformation)
        TextBox1.Text = ""

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

        TextBox2.Text = totalCartPrice

        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        resetstock()
        CartItems()
        StocksItems()
        TextBox3.Text = ""
        TextBox4.Text = ""
        MessageBox.Show("Cart cleared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim forSukli As Integer = TextBox3.Text - TextBox2.Text

        If Integer.TryParse(TextBox3.Text, Nothing) Then
            If TextBox3.Text >= TextBox2.Text Then
                TextBox4.Text = forSukli
            Else
                MessageBox.Show("Kulanggg")
            End If
        Else
            MessageBox.Show("lagyan mo boss")
        End If


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

End Class