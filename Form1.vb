Imports MySql.Data.MySqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
        AddHandler TextBox1.KeyPress, AddressOf NumberOnly_KeyPress
        AddHandler TextBox4.KeyPress, AddressOf NumberOnly_KeyPress
        AddHandler TextBox5.KeyPress, AddressOf NumberOnly_KeyPress

        AddHandler TextBox2.KeyPress, AddressOf TextOnly_KeyPress
        AddHandler TextBox3.KeyPress, AddressOf TextOnly_KeyPress
        AddHandler TextBox6.KeyPress, AddressOf TextOnly_KeyPress
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
            ClearFields()
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
            ClearFields()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Validate input fields
        Dim student_id = TextBox1.Text.Trim()
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
        Dim validate As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If validate = DialogResult.No Then
            Return
        End If
        Try
            If String.IsNullOrWhiteSpace(student_id) Then
                MessageBox.Show("Please enter a Student ID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            con.Open()
            query = "DELETE FROM students_profile WHERE student_id = @student_id"
            command = New MySqlCommand(query, con)

            command.Parameters.AddWithValue("@student_id", student_id)
            command.ExecuteNonQuery()

            MessageBox.Show("Data record deleted successfully!")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
            LoadData()
            ClearFields()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim student_id = TextBox5.Text.Trim()
        If TextBox5.Text = "" Then
            MsgBox("PLEASE INPUT A STUDENT ID", MsgBoxStyle.Critical, "Error")
        End If
        If student_id = "" Then
            ' Clear fields if ID is empty
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox6.Text = ""
            DateTimePicker1.Value = DateTime.Now
            Return
        End If

        Try
            con.Open()
            query = "SELECT * FROM students_profile WHERE student_id = @student_id"
            command = New MySqlCommand(query, con)
            command.Parameters.AddWithValue("@student_id", student_id)
            dataReader = command.ExecuteReader()
            If dataReader.Read() Then
                TextBox1.Text = dataReader("student_id").ToString()
                TextBox2.Text = dataReader("first_name").ToString()
                TextBox3.Text = dataReader("last_name").ToString()
                TextBox4.Text = dataReader("age").ToString()
                TextBox6.Text = dataReader("address").ToString()
                DateTimePicker1.Value = Convert.ToDateTime(dataReader("birthday"))
            Else
                ' Clear fields if not found
                MessageBox.Show("Student ID not found.")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox6.Text = ""
                DateTimePicker1.Value = DateTime.Now
            End If
            dataReader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
    Private Sub ClearFields()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox6.Text = ""
        DateTimePicker1.Value = DateTime.Now
        TextBox5.Text = ""
    End Sub
End Class