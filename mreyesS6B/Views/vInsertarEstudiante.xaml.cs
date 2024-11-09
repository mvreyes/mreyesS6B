using System.Net;

namespace mreyesS6B.Views;

public partial class vInsertarEstudiante : ContentPage
{
	public vInsertarEstudiante()
	{
		InitializeComponent();
	}

    private void btnGuardar_Clicked(object sender, EventArgs e)
    {
		try
		{
			WebClient cliente = new WebClient();
			var parametros = new System.Collections.Specialized.NameValueCollection();
			parametros.Add("nombre", txtNombre.Text);
			parametros.Add("apellido", txtApellido.Text);
			parametros.Add("edad", txtEdad.Text);

			cliente.UploadValues("http://127.0.0.1/wsestudiantes/estudiante.php", "POST", parametros);
			Navigation.PushAsync(new vEstudiante());
		}
		catch (Exception ex)
		{
			DisplayAlert("ERROR", ex.Message, "Cerrar");
		}
    }
}