Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=db_students;port=3306;")
    Public ds As DataSet = New DataSet()
    Public da As MySqlDataAdapter
    Public dt As DataTable = New DataTable()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Format TextBox1 and TextBox4 to accept numbers only  
        AddHandler TextBox1.KeyPress, AddressOf NumberOnly_KeyPress
        AddHandler TextBox4.KeyPress, AddressOf NumberOnly_KeyPress

        ' Format TextBox2, TextBox3, and TextBox6 to accept normal text  
        AddHandler TextBox2.KeyPress, AddressOf TextOnly_KeyPress
        AddHandler TextBox3.KeyPress, AddressOf TextOnly_KeyPress
        AddHandler TextBox6.KeyPress, AddressOf TextOnly_KeyPress
    End Sub

    Private Sub NumberOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow only numbers and control keys  
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow only letters, spaces, and control keys  
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class
