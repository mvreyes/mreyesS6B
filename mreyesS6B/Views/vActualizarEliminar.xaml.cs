using mreyesS6B.Models;

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

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }
}