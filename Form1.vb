Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    ' load data from database
    Public Sub LoadData()
        Try
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim student_id = TextBox1.Text.Trim()
        Dim first_name = TextBox2.Text.Trim()
        Dim last_name = TextBox3.Text.Trim()
        Dim age = TextBox4.Text.Trim()
        Dim address = TextBox6.Text.Trim()
        Dim birthday = DateTimePicker1.Value.ToString("yyyy-MM-dd")

        ' Check for empty fields
        If String.IsNullOrWhiteSpace(first_name) OrElse
           String.IsNullOrWhiteSpace(last_name) OrElse
           String.IsNullOrWhiteSpace(age) OrElse
           String.IsNullOrWhiteSpace(address) Then
            MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            con.Open()
            query = "INSERT INTO students_profile (student_id, first_name, last_name, age, address, birthday) VALUES (@student_id, @first_name, @last_name, @age, @address, @birthday)"
            command = New MySqlCommand(query, con)

            command.Parameters.AddWithValue("@student_id", student_id)
            command.Parameters.AddWithValue("@first_name", first_name)
            command.Parameters.AddWithValue("@last_name", last_name)
            command.Parameters.AddWithValue("@age", age)
            command.Parameters.AddWithValue("@address", address)
            command.Parameters.AddWithValue("@birthday", birthday)
            command.ExecuteNonQuery()
            MessageBox.Show("Data inserted successfully!")
            LoadData()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Validate input fields
        Dim student_id = TextBox1.ReadOnly = True
        Dim first_name = TextBox2.Text.Trim()
        Dim last_name = TextBox3.Text.Trim()
        Dim age = TextBox4.Text.Trim()
        Dim address = TextBox6.Text.Trim()
        Dim birthday = DateTimePicker1.Value.ToString("yyyy-MM-dd")

        ' Check for empty fields
        If String.IsNullOrWhiteSpace(student_id) OrElse
           String.IsNullOrWhiteSpace(first_name) OrElse
           String.IsNullOrWhiteSpace(last_name) OrElse
           String.IsNullOrWhiteSpace(age) OrElse
           String.IsNullOrWhiteSpace(address) Then
            MessageBox.Show("All fields must be filled out.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            con.Open()
            query = "UPDATE students_profile SET first_name = @first_name, last_name = @last_name, age = @age, address = @address, birthday = @birthday WHERE student_id = @student_id"
            command = New MySqlCommand(query, con)

            command.Parameters.AddWithValue("@student_id", student_id)
            command.Parameters.AddWithValue("@first_name", first_name)
            command.Parameters.AddWithValue("@last_name", last_name)
            command.Parameters.AddWithValue("@age", age)
            command.Parameters.AddWithValue("@address", address)
            command.Parameters.AddWithValue("@birthday", birthday)
            command.ExecuteNonQuery()
            MessageBox.Show("Data updated successfully!")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
            LoadData()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class