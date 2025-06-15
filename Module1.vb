Imports MySql.Data.MySqlClient

Module Module1

    Public con As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=db_students;port=3306;")
    Public dataAdapter As MySqlDataAdapter
    Public dataReader As MySqlDataReader
    Public command As MySqlCommand = New MySqlCommand()
    Public datSet As DataSet = New DataSet()
    Public dataTable As DataTable = New DataTable()
    Public query As String

    Public Sub LoadData()
        Try
            Dim query As String = "SELECT * FROM students_profile"
            dataAdapter = New MySqlDataAdapter(query, con)
            dataTable.Clear()
            dataAdapter.Fill(dataTable)
            Form1.DataGridView1.DataSource = dataTable
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

End Module
