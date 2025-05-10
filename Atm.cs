using System;
using System.Collections.Generic;

class User
{
public string Name { get; set; }
public string Pin { get; set; }
public double Balance { get; set; }
public List<string> Transactions { get; set; }


public User(string name, string pin, double balance)
{
    Name = name;
    Pin = pin;
    Balance = balance;
    Transactions = new List<string>();
}



}

class ATM
{
static Dictionary<string, User> users = new Dictionary<string, User>()
{
{ "1234", new User("Surendar", "1234", 2000.0) },
{ "5678", new User("John", "5678", 1500.0) }
};

static User currentUser = null;

static void Main()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("===== Welcome to Advanced Console ATM =====");
        Console.Write("Enter your PIN: ");
        string pin = Console.ReadLine();

        if (users.ContainsKey(pin))
        {
            currentUser = users[pin];
            Console.WriteLine($"Welcome, {currentUser.Name}!");
            Pause();
            ShowMenu();
        }
        else
        {
            Console.WriteLine("Invalid PIN. Try again.");
            Pause();
        }
    }
}

static void ShowMenu()
{
    bool loggedIn = true;

    while (loggedIn)
    {
        Console.Clear();
        Console.WriteLine($"=== ATM Menu - Logged in as {currentUser.Name} ===");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Deposit");
        Console.WriteLine("3. Withdraw");
        Console.WriteLine("4. Transaction History");
        Console.WriteLine("5. Change PIN");
        Console.WriteLine("6. Logout");
        Console.Write("Select an option: ");

        switch (Console.ReadLine())
        {
            case "1":
                CheckBalance();
                break;
            case "2":
                Deposit();
                break;
            case "3":
                Withdraw();
                break;
            case "4":
                ShowTransactionHistory();
                break;
            case "5":
                ChangePin();
                break;
            case "6":
                Console.WriteLine("Logging out...");
                currentUser = null;
                loggedIn = false;
                break;
            default:
                Console.WriteLine("Invalid option.");
                Pause();
                break;
        }
    }
}

static void CheckBalance()
{
    Console.WriteLine($"\\nYour current balance is: ${currentUser.Balance:F2}");
    Pause();
}

static void Deposit()
{
    Console.Write("\\nEnter amount to deposit: $");
    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
    {
        currentUser.Balance += amount;
        currentUser.Transactions.Add($"Deposited ${amount:F2} on {DateTime.Now}");
        Console.WriteLine($"Deposited ${amount:F2}");
    }
    else
    {
        Console.WriteLine("Invalid amount.");
    }
    Pause();
}

static void Withdraw()
{
    Console.Write("\\nEnter amount to withdraw: $");
    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
    {
        if (amount <= currentUser.Balance)
        {
            currentUser.Balance -= amount;
            currentUser.Transactions.Add($"Withdrew ${amount:F2} on {DateTime.Now}");
            Console.WriteLine($"Withdrew ${amount:F2}");
        }
        else
        {
            Console.WriteLine("Insufficient balance.");
        }
    }
    else
    {
        Console.WriteLine("Invalid amount.");
    }
    Pause();
}

static void ShowTransactionHistory()
{
    Console.WriteLine("\\n--- Transaction History ---");
    if (currentUser.Transactions.Count == 0)
    {
        Console.WriteLine("No transactions found.");
    }
    else
    {
        foreach (string txn in currentUser.Transactions)
        {
            Console.WriteLine(txn);
        }
    }
    Pause();
}

static void ChangePin()
{
    Console.Write("\\nEnter your current PIN: ");
    string oldPin = Console.ReadLine();

    if (oldPin == currentUser.Pin)
    {
        Console.Write("Enter new PIN: ");
        string newPin = Console.ReadLine();

        Console.Write("Confirm new PIN: ");
        string confirmPin = Console.ReadLine();

        if (newPin == confirmPin && !string.IsNullOrWhiteSpace(newPin))
        {
            users.Remove(currentUser.Pin); // Remove old entry
            currentUser.Pin = newPin;
            users[newPin] = currentUser;   // Add with new PIN
            Console.WriteLine("PIN successfully changed!");
        }
        else
        {
            Console.WriteLine("PINs do not match or are invalid.");
        }
    }
    else
    {
        Console.WriteLine("Incorrect current PIN.");
    }
    Pause();
}

static void Pause()
{
    Console.WriteLine("\\nPress Enter to continue...");
    Console.ReadLine();
}


}
