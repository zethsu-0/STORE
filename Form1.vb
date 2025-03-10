Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StocksTableAdapter.Fill(Me.STOREDataSet1.Stocks)
        Opencon()
        MessageBox.Show("STORE OPENS")
        con.Close()


        CartItems()
        StocksItems()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Opencon()

        Dim Item_no As String = TextBox1.Text
        Dim Product_name As String = String.Empty
        Dim Quantity As Integer = 0
        Dim Price As Decimal = 0

        Dim checkQuery As String = "SELECT Product_name, Quantity, Price FROM stocks WHERE Item_no = @Item_no"
        Using selectCmd As New SqlCommand(checkQuery, con)
            selectCmd.Parameters.AddWithValue("@Item_no", Item_no)

            Using reader As SqlDataReader = selectCmd.ExecuteReader()
                If reader.Read() Then
                    Product_name = reader("Product_name").ToString()
                    Quantity = Convert.ToInt32(reader("Quantity"))
                    Price = Convert.ToDecimal(reader("Price"))

                Else
                    MsgBox("Item not found in stocks.", vbCritical)
                    con.Close()
                    Return
                End If
            End Using
        End Using


        Dim checkCartQuery As String = "SELECT Quantity, Price FROM Cart WHERE Item_No = @Item_no"
        Using checkCartCmd As New SqlCommand(checkCartQuery, con)
            checkCartCmd.Parameters.AddWithValue("@Item_no", Item_no)

            Dim existingQuantity As Integer = 0
            Dim existingPrice As Decimal = 0
            Dim cartReader As SqlDataReader = checkCartCmd.ExecuteReader()

            If cartReader.Read() Then

                existingQuantity = Convert.ToInt32(cartReader("Quantity"))
                existingPrice = Convert.ToDecimal(cartReader("Price"))
                cartReader.Close()


                Dim newQuantity As Integer = existingQuantity + 1
                Dim TotalPrice As Decimal = newQuantity * Price


                Dim updateQuery As String = "UPDATE Cart SET Quantity = @NewQuantity, Price = @TotalPrice WHERE Item_No = @Item_no"
                Using updateCmd As New SqlCommand(updateQuery, con)
                    updateCmd.Parameters.AddWithValue("@NewQuantity", newQuantity)
                    updateCmd.Parameters.AddWithValue("@TotalPrice", TotalPrice)
                    updateCmd.Parameters.AddWithValue("@Item_no", Item_no)
                    updateCmd.ExecuteNonQuery()
                End Using
            Else

                cartReader.Close()

                Dim totalPrice As Decimal = 1 * Price

                Dim insertQuery As String = "INSERT INTO Cart (ItemNo, Product_name, Quantity, Price) VALUES (@Item_no, @Product_name, @Quantity, @Price)"
                Using insertCmd As New SqlCommand(insertQuery, con)
                    insertCmd.Parameters.AddWithValue("@Item_no", Item_no)
                    insertCmd.Parameters.AddWithValue("@Product_name", Product_name)
                    insertCmd.Parameters.AddWithValue("@Quantity", 1)
                    insertCmd.Parameters.AddWithValue("@Price", totalPrice)
                    insertCmd.ExecuteNonQuery()
                End Using
            End If

        End Using

        MsgBox("Item added to Cart successfully.", vbInformation)
        TextBox1.Text = ""

        CartItems()

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
End Class