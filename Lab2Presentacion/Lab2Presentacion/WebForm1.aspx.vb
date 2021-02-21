Public Class WebForm1
    Inherits System.Web.UI.Page
    Public Shared ln As New LogicaNegocio.Class1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RequiredFieldValidator1.Validate()
        RequiredFieldValidator2.Validate()

        If (ln.existeEmailConfirmado(TextBox1.Text)) Then
            If (ln.emailContraseña(TextBox1.Text, TextBox2.Text)) Then 'Si la contraseña corresponde al email introducido
                Response.Redirect(“PaginaUsuarioIniciado.aspx")
            Else
                Label4.Visible = True
                Label4.Text = "Contraseña Erronea."
            End If
        Else
            Label4.Visible = True
            Label4.Text = "El email es erroneo."
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect(“registro.aspx")
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Response.Redirect(“CambioContrasena.aspx")
    End Sub
End Class