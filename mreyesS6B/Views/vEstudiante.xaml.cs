using mreyesS6B.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace mreyesS6B.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://127.0.0.1/wsestudiantes/estudiante.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> estud;

	public vEstudiante()
	{
		InitializeComponent();
		Listar();
	}

	public async void Listar()
	{ 
		var content = await cliente.GetStringAsync(Url);
		List<Estudiante> ListEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		estud = new ObservableCollection<Estudiante>(ListEst);
        lvEstudiantes.ItemsSource = estud;
	}
}