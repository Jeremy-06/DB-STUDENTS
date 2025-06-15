Imports MySql.Data.MySqlClient

Public Class Form1
    Public con As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=db_students;port=3306;")
    Public datSet As DataSet = New DataSet()
    Public dataAdapter As MySqlDataAdapter
    Public dataTable As DataTable = New DataTable()
    Public dataReader As MySqlDataReader

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    Public Sub LoadData()
        Try
            con.Open()
            Dim query As String = "SELECT * FROM students_profile"
            dataAdapter = New MySqlDataAdapter(query, con)
            dataTable.Clear()
            dataAdapter.Fill(dataTable)
            DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

End Class