Imports System.Data.Common
Imports System.Data.Odbc
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1

    Dim Conn As OdbcConnection
    Dim cmd As OdbcCommand
    Dim Ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim Rd As OdbcDataReader
    Dim MyDB As String

    Sub koneksi()
        MyDB = "DSN=DSDbGudang;Driver={MySQL ODBC 8.0 ANSI Driver};Database=akademik_db;server=localhost;uid=root"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()

    End Sub

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Delete"
        Button4.Text = "Close"

        Call koneksi()
        Da = New OdbcDataAdapter("select * from mahasiswa", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "mahasiswa")
        DataGridView1.DataSource = Ds.Tables("mahasiswa")
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Silahkan mengisi data secara lengkap")
        Else
            'Query sql
            Dim sql As String = "SELECT * FROM mahasiswa WHERE nim = " & TextBox1.Text
            'Perintah buat sql nya
            Dim sqlQuery As New OdbcCommand(sql, Conn)
            Dim Reader As OdbcDataReader = sqlQuery.ExecuteReader()

            If Reader.HasRows Then
                MsgBox("NIM sudah terdaftar", MsgBoxStyle.Exclamation, "Input gagal")
            Else
                Dim InputData As String = "insert into mahasiswa values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "', '" & ComboBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "' )"
                cmd = New OdbcCommand(InputData, Conn)
                cmd.ExecuteNonQuery()
                MsgBox("Data berhasil terdaftar", MsgBoxStyle.Information, "Input berhasil")
            End If


            Call KondisiAwal()

            Reader.Close()

        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Silahkan mengisi data secara lengkap")
        Else
            Dim sql As String = $"UPDATE mahasiswa SET nama_mahasiswa = '{TextBox2.Text}', jkel = '{ComboBox1.Text}', prodi= '{ComboBox2.Text}', alamat = '{TextBox3.Text}', nohp = '{TextBox4.Text}' WHERE nim = '{TextBox1.Text}'"
            cmd = New OdbcCommand(sql, Conn)
            cmd.ExecuteNonQuery()
            MsgBox("Edit berhasil", MsgBoxStyle.Information, "Edit Succes")
            Call KondisiAwal()
        End If

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Silahkan mengisi data secara lengkap")
        Else

            Call koneksi()
            Dim DeleteData As String = "delete from mahasiswa where nim='" & TextBox1.Text & "'"
            cmd = New OdbcCommand(DeleteData, Conn)
            cmd.ExecuteNonQuery()
            MsgBox("Delete Data Berhasil")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            cmd = New OdbcCommand("select * from mahasiswa where nim = '" & TextBox1.Text & "'", Conn)
            Rd = cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("nama_mahasiswa")
                ComboBox1.Text = Rd.Item("jkel")
                ComboBox2.Text = Rd.Item("prodi")
                TextBox3.Text = Rd.Item("alamat")
                TextBox4.Text = Rd.Item("nohp")
            Else
                MsgBox("Data tidak ditemukan")
            End If
        End If
    End Sub
End Class
