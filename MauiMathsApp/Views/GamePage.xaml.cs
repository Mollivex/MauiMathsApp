namespace MauiMathsApp.Views;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;

    public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		CreateNewQuestion();
	}

	private void CreateNewQuestion()
	{
		var gameOperand = GameType switch
		{
			"Addition" => "+",
			"Substraction" => "-",
			"Multiplication" => "*",
            "Division" => "/",
			_ => ""
        };

		var random = new Random();

		// Ternary operator
		firstNumber = GameType != "Division" ? random.Next(1, 999) : random.Next(1, 999);
		secondNumber = GameType != "Division" ? random.Next(1, 99) : random.Next(1, 99);

        firstNumber = GameType != "Multiplication" ? random.Next(1, 999) : random.Next(1, 99);
        secondNumber = GameType != "Multiplication" ? random.Next(1, 99) : random.Next(1, 9);


        if (GameType == "Division")
		{
			while(firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
				firstNumber = random.Next(1, 999);
				secondNumber = random.Next(1, 99);
			}
		}

		QuestionLabel.Text = $"{firstNumber} {gameOperand} {secondNumber}";
    }

    private void SubmitAnswer_Clicked(object sender, EventArgs e)
    {

    }
}