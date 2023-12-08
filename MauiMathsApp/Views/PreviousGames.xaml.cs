using MauiMathsApp.Models;

namespace MauiMathsApp;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
	{
		InitializeComponent();
        App.GameRepository.GetAllGames();
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

    private void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        App.GameRepository.Delete((int)button.BindingContext);
        gamesList.ItemsSource = App.GameRepository.GetAllGames();
    }
}