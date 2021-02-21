Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.Strings
Public Class accesodatosSQL
    Private Shared conexion As New SqlConnection
    Private Shared comando As New SqlCommand
    Public Shared Function conectar() As String
        Try
            conexion.ConnectionString = “"
            conexion.Open()
        Catch ex As Exception
            Return "ERROR DE CONEXIÓN: " + ex.Message
        End Try
        Return "CONEXION OK"
    End Function

    Public Shared Sub cerrarconexion()
        conexion.Close()
    End Sub

    Public Shared Function insertar(ByVal email As String, ByVal nombre As String, ByVal apellidos As String, ByVal numbconfir As Integer, ByVal confirmado As Boolean, ByVal tipo As String, ByVal pass As String, ByVal codpass As Integer) As String
        Dim st = "insert into Usuarios (email,nombre,apellidos, numconfir, confirmado, tipo, pass, codpass) values ('" & email & " ','" & nombre & " ','" & apellidos & " ', '" & numbconfir & " ','" & confirmado & " ','" & tipo & " ','" & pass & " ','" & codpass & " ')"
        Dim numregs As Integer
        comando = New SqlCommand(st, conexion)
        Try
            numregs = comando.ExecuteNonQuery()
        Catch ex As Exception
            Return ex.Message
        End Try
        Return (numregs & " registro(s) insertado(s) en la BD ")
    End Function

    Public Shared Function emailycodigoCorrectos(ByVal emailto As String, ByVal codigo As String) As Boolean
        Try
            Dim st = "select * from Usuarios"
            comando = New SqlCommand(st, conexion)
            Dim datos As SqlDataReader
            datos = comando.ExecuteReader()
            Dim confirmar As Boolean = False
            While datos.Read
                Dim emailespacio As String = emailto + " "
                If ((String.Compare(emailespacio, datos.Item("email")) = 0) And (String.Compare(codigo, datos.Item("numconfir").ToString) = 0)) Then
                    confirmar = True
                End If
            End While
            Return confirmar
        Catch ex As SqlException
            Console.WriteLine("SQL error.")
            Return False
        Catch ex As Exception
            Console.WriteLine("Exception")
            Return False
        Finally
            Console.WriteLine("Finally")
        End Try
    End Function

    Public Shared Function cambioConfirmado(ByVal email As String) As Boolean
        Try
            Dim tr As Boolean = True
            Dim st2 = "update Usuarios set confirmado = '" & tr & " ' where email = '" & email & " '"
            comando = New SqlCommand(st2, conexion)
            comando.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function codigoContraseña(ByVal email As String, ByVal codigo As Integer) As Boolean
        Try
            Dim st2 = "update Usuarios set codpass = '" & codigo & " ' where email = '" & email & " '"
            comando = New SqlCommand(st2, conexion)
            comando.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function existeEmailConfirmado(ByVal emailto As String) As Boolean
        Try
            Dim conf As Boolean = True
            Dim st = "select * from Usuarios where confirmado = '" & conf & " '"
            comando = New SqlCommand(st, conexion)
            Dim datos As SqlDataReader
            datos = comando.ExecuteReader()
            Dim confirmar As Boolean = False
            While datos.Read
                Dim emailespacio As String = emailto + " "
                If ((String.Compare(emailespacio, datos.Item("email")) = 0)) Then
                    confirmar = True
                End If
            End While
            Return confirmar
        Catch ex As Exception
            Console.WriteLine("Exception")
            Return False
        End Try
    End Function

    Public Shared Function existeEmail(ByVal emailto As String) As Boolean
        Try
            Dim st = "select * from Usuarios"
            comando = New SqlCommand(st, conexion)
            Dim datos As SqlDataReader
            datos = comando.ExecuteReader()
            Dim existe As Boolean = False
            While datos.Read
                Dim emailespacio As String = emailto + " "
                If ((String.Compare(emailespacio, datos.Item("email")) = 0)) Then
                    existe = True
                End If
            End While
            Return existe
        Catch ex As Exception
            Console.WriteLine("Exception")
            Return False
        End Try
    End Function

    Public Shared Function codigoContraseñaGet(ByVal emailto As String) As (numero As Integer, confir As Boolean)
        Try
            Dim emailespacio As String = emailto + " "
            Dim st = "select * from Usuarios where email = '" & emailto & "'"
            comando = New SqlCommand(st, conexion)
            Dim datos As SqlDataReader
            datos = comando.ExecuteReader()
            Dim numero As Integer = -1
            While datos.Read
                numero = datos.Item("codpass")
            End While
            Return (numero, True)
        Catch ex As Exception
            Console.WriteLine("Exception")
            Return (-1, False)
        End Try
    End Function

    Public Shared Function cambiarContraseña(ByVal email As String, ByVal pass As String) As Boolean
        Try
            Dim st2 = "update Usuarios set pass = '" & pass & " ' where email = '" & email & " '"
            comando = New SqlCommand(st2, conexion)
            comando.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function emailContraseña(ByVal emailto As String, ByVal pass As String) As Boolean
        Try
            Dim st = "select * from Usuarios where email = '" & emailto & "'"
            comando = New SqlCommand(st, conexion)
            Dim datos As SqlDataReader
            datos = comando.ExecuteReader()
            Dim passconespacio As String = pass + " "
            While datos.Read
                If ((String.Compare(passconespacio, datos.Item("pass")) = 0)) Then
                    Return True
                End If
            End While
            Return False
        Catch ex As Exception
            Console.WriteLine("Exception")
            Return False
        End Try
    End Function

End Class
