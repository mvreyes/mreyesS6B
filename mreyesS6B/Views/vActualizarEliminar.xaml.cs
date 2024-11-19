using mreyesS6B.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace mreyesS6B.Views;

public partial class vActualizarEliminar : ContentPage
{
	public vActualizarEliminar()
	{
		InitializeComponent();
	}


    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();

    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verifica que todos los campos tengan datos válidos
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlert("ERROR", "Todos los campos son obligatorios.", "Cerrar");
                return;
            }

            using (WebClient cliente = new WebClient())
            {
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);

                // Cambia 127.0.0.1 por la dirección IP de tu computadora en la red local
                var response = cliente.UploadValues("http://127.0.0.1/wsestudiantes/estudiante.php", "PUT", parametros);

                // Convierte la respuesta a string para verificar el resultado
                string responseString = Encoding.UTF8.GetString(response);

                // Muestra la respuesta del servidor para diagnóstico
                await DisplayAlert("Respuesta del Servidor", responseString, "Cerrar");

                // Verifica si la respuesta indica éxito
                if (responseString.Contains("success"))
                {
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("ERROR", "No se pudo actualizar el estudiante.", "Cerrar");
                }
            }
        }
        catch (WebException webEx)
        {
            // Manejo de errores para WebException
            var response = (HttpWebResponse)webEx.Response;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();
                await DisplayAlert("ERROR", $"{webEx.Message}\n{responseText}", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores generales
            await DisplayAlert("ERROR", ex.Message, "Cerrar");
        }
    }


    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verifica que el código del estudiante esté presente
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                await DisplayAlert("ERROR", "El código del estudiante es obligatorio.", "Cerrar");
                return;
            }

            // Pregunta al usuario si está seguro de eliminar
            bool confirm = await DisplayAlert("Confirmar", "¿Estás seguro de que deseas eliminar este estudiante?", "Sí", "No");
            if (!confirm)
                return;

            using (WebClient cliente = new WebClient())
            {
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("codigo", txtCodigo.Text);

                // Cambia 127.0.0.1 por la dirección IP de tu computadora en la red local
                var response = cliente.UploadValues("http://127.0.0.1/wsestudiantes/estudiante.php", "DELETE", parametros);

                string responseString = Encoding.UTF8.GetString(response);
                await DisplayAlert("Respuesta del Servidor", responseString, "Cerrar");

                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                if (jsonResponse.ContainsKey("status") && jsonResponse["status"] == "success")
                {
                    await DisplayAlert("Éxito", "Estudiante eliminado correctamente.", "Cerrar");
                    // Aquí podrías navegar a otra página o actualizar la lista de estudiantes
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    await DisplayAlert("ERROR", jsonResponse["message"], "Cerrar");
                }
            }
        }
        catch (WebException webEx)
        {
            var response = (HttpWebResponse)webEx.Response;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string responseText = reader.ReadToEnd();
                await DisplayAlert("ERROR", $"{webEx.Message}\n{responseText}", "Cerrar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", ex.Message, "Cerrar");
        }
    }
}