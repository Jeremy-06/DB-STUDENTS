Imports MySql.Data.MySqlClient

Module Module1

    Public con As MySqlConnection = New MySqlConnection("server=localhost;user id=root;password=;database=db_students;port=3306;")
    Public dataAdapter As MySqlDataAdapter
    Public dataReader As MySqlDataReader
    Public command As MySqlCommand = New MySqlCommand()
    Public datSet As DataSet = New DataSet()
    Public dataTable As DataTable = New DataTable()
    Public query As String

End Module
