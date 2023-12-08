using MauiMathsApp.Models;

namespace MauiMathsApp.Views;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;
	const int totalQuestions = 10;
	int gamesLeft = totalQuestions;

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
		try
		{
            var answer = Int32.Parse(AnswerEntry.Text);

            bool isCorrect = false;

            switch (GameType)
            {
                case "Addition":
                    isCorrect = answer == firstNumber + secondNumber;
                    break;
                case "Substraction":
                    isCorrect = answer == firstNumber - secondNumber;
                    break;
                case "Multiplication":
                    isCorrect = answer == firstNumber * secondNumber;
                    break;
                case "Division":
                    isCorrect = answer == firstNumber / secondNumber;
                    break;
            }
            ProcessAnswer(isCorrect);

            gamesLeft--;
            AnswerEntry.Text = "";

            if (gamesLeft > 0)
                CreateNewQuestion();
            else
                GameOver();
        }
		catch (Exception)
		{

		}
    }

	private void GameOver()
	{
		GameOperation gameOperation = GameType switch
        {
            "Addition" => GameOperation.Addition,
            "Substraction" => GameOperation.Substraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division,
            _ => throw new NotImplementedException(),
        };

		QuestionArea.IsVisible = false;
		BackToMenuBtn.IsVisible = true;
        GameOverLabel.Text = $"Game over! Your got {score} out of {totalQuestions} right";

		App.GameRepository.Add(new Game
		{
			DatePlayed = DateTime.Now,
			Type = gameOperation,
			Score = score
		});
	}

    private void ProcessAnswer(bool isCorrect)
    {
		if (isCorrect)
			score += 1;

		AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect!";
    }

    private void BackToMenu_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MainPage());
    }
}