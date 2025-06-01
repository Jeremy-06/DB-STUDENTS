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

        ' Load data into DataGridView  
        LoadDataIntoGridView()
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            ' Ensure the selected row is valid  
            If e.RowIndex >= 0 Then
                ' Get the selected row  
                Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

                ' Populate the text boxes with the selected row's data  
                TextBox1.Text = selectedRow.Cells("student_id").Value.ToString()
                TextBox2.Text = selectedRow.Cells("first_name").Value.ToString()
                TextBox3.Text = selectedRow.Cells("last_name").Value.ToString()
                TextBox4.Text = selectedRow.Cells("age").Value.ToString()
                DateTimePicker1.Value = DateTime.Parse(selectedRow.Cells("birthday").Value.ToString())
                TextBox6.Text = selectedRow.Cells("address").Value.ToString()
            End If
        Catch ex As Exception
            ' Handle errors  
            MessageBox.Show("An error occurred while selecting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDataIntoGridView()
        Try
            ' Prepare the SQL query to fetch data  
            Dim query As String = "SELECT * FROM students_profile"
            da = New MySqlDataAdapter(query, conn)

            ' Fill the DataSet and bind it to the DataGridView  
            ds.Clear()
            da.Fill(ds, "students")
            DataGridView1.DataSource = ds.Tables("students")
        Catch ex As Exception
            ' Handle errors  
            MessageBox.Show("An error occurred while loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection  
            conn.Close()
        End Try
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Validate input fields  
            If String.IsNullOrWhiteSpace(TextBox2.Text) OrElse String.IsNullOrWhiteSpace(TextBox3.Text) Then
                MessageBox.Show("First Name and Last Name cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Open the connection  
            conn.Open()

            ' Prepare the SQL query to insert data  
            Dim query As String = "INSERT INTO students_profile (student_id, first_name, last_name, age, birthday, address) VALUES (@id, @firstName, @lastName, @age, @birthday, @address)"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)

            ' Add parameters to the query  
            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@firstName", TextBox2.Text)
            cmd.Parameters.AddWithValue("@lastName", TextBox3.Text)
            cmd.Parameters.AddWithValue("@age", TextBox4.Text)
            cmd.Parameters.AddWithValue("@birthday", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@address", TextBox6.Text)

            ' Execute the query  
            cmd.ExecuteNonQuery()

            ' Notify the user  
            MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the DataGridView  
            LoadDataIntoGridView()
        Catch ex As Exception
            ' Handle errors  
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection  
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Validate input fields  
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Student ID cannot be blank.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Open the connection  
            conn.Open()

            ' Prepare the SQL query to update data  
            Dim query As String = "UPDATE students_profile SET first_name = @firstName, last_name = @lastName, age = @age, birthday = @birthday, address = @address WHERE student_id = @id"
            Dim cmd As MySqlCommand = New MySqlCommand(query, conn)

            ' Add parameters to the query  
            cmd.Parameters.AddWithValue("@id", TextBox1.Text)
            cmd.Parameters.AddWithValue("@firstName", TextBox2.Text)
            cmd.Parameters.AddWithValue("@lastName", TextBox3.Text)
            cmd.Parameters.AddWithValue("@age", TextBox4.Text)
            cmd.Parameters.AddWithValue("@birthday", DateTimePicker1.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@address", TextBox6.Text)

            ' Execute the query  
            cmd.ExecuteNonQuery()

            ' Notify the user  
            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the DataGridView  
            LoadDataIntoGridView()
        Catch ex As Exception
            ' Handle errors  
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the connection  
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
End Class
