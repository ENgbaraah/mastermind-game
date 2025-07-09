using System;

class Mastermind
{
    static void Main(string[] args)
    {
        string secretCode = GetSecretCode(args);
        int attempts = GetAttempts(args);

        Console.WriteLine("Can you break the code?");
        Console.WriteLine("Enter a valid guess.");

        int currentRound = 0;

        while (currentRound < attempts)
        {
            Console.WriteLine("Round " + currentRound);
            Console.Write("> ");
            string guess = Console.ReadLine();

            if (guess == null)
            {
                Console.WriteLine("End of input detected. Exiting.");
                break;
            }

            if (!IsValidGuess(guess))
            {
                Console.WriteLine("Wrong input!");
                continue;
            }

            if (guess == secretCode)
            {
                Console.WriteLine("Congratz! You did it!");
                return;
            }

            int wellPlaced = CountWellPlaced(secretCode, guess);
            int misplaced = CountMisplaced(secretCode, guess);

            Console.WriteLine("Well-placed pieces: " + wellPlaced);
            Console.WriteLine("Misplaced pieces: " + misplaced);

            currentRound++;
        }

        Console.WriteLine("Out of attempts! The code was: " + secretCode);
    }

    static string GetSecretCode(string[] args)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "-c")
            {
                if (IsValidGuess(args[i + 1]))
                {
                    return args[i + 1];
                }
            }
        }
        return GenerateRandomCode();
    }

    static int GetAttempts(string[] args)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "-t")
            {
                if (int.TryParse(args[i + 1], out int t))
                {
                    return t;
                }
            }
        }
        return 10;
    }

    static string GenerateRandomCode()
    {
        Random rnd = new Random();
        string code = "";
        while (code.Length < 4)
        {
            char c = (char)('0' + rnd.Next(0, 9));
            if (!code.Contains(c))
            {
                code += c;
            }
        }
        return code;
    }

    static bool IsValidGuess(string guess)
    {
        if (guess.Length != 4)
        {
            return false;
        }

        foreach (char c in guess)
        {
            if (c < '0' || c > '8')
            {
                return false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (guess[i] == guess[j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    static int CountWellPlaced(string secret, string guess)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (secret[i] == guess[i])
            {
                count++;
            }
        }
        return count;
    }

    static int CountMisplaced(string secret, string guess)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i != j && secret[i] == guess[j])
                {
                    count++;
                }
            }
        }
        return count;
    }
}
// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
