// Student Task Manager - Tutorial Steps 1 to 6
// Step 1: Console output       Step 2: Variables, input, constants
// Step 3: if / switch          Step 4: Lists and loops
// Step 5: Methods              Step 6: Enums and final integration

// ---------- Constants (Step 2 and Step 6) ----------
const string AppName = "Student Task Manager";
const int MaxTasks = 10; // maximum tasks allowed per day / in the list

// ---------- Step 1: Hello, Console ----------
// TODO: edit the three personal lines below so they are true for you.
Console.WriteLine(AppName);
Console.WriteLine("====================");
Console.WriteLine("Welcome to the application!");
Console.WriteLine("Name: Ahmed");
Console.WriteLine("Favorite programming language: C#");
Console.WriteLine("Why I want to learn .NET: to build real applications for my Applied Informatics studies.");

// Investigate: Write() vs WriteLine()
Console.Write("Write() stays on the same line... ");
Console.Write("see?");
Console.WriteLine();
Console.WriteLine("WriteLine() moves to the next line after printing.");

// ---------- Step 2: Variables, input, output ----------
Console.WriteLine();
string studentName = ReadText("Enter your name: ");
int age = ReadInt("Enter your age: ");
string courseName = ReadText("Enter your course name: ");
bool knowsCSharp = ReadYesNo("Do you already know C#? (y/n): ");
int taskGoal = ReadInt("How many tasks do you want to complete today? ");
double availableHours = ReadDouble("How many hours do you have available? ");

Console.WriteLine();
Console.WriteLine($"Hello {studentName}!");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Course: {courseName}");
Console.WriteLine($"Already knows C#: {knowsCSharp}");
Console.WriteLine($"Your goal is {taskGoal} tasks.");
Console.WriteLine($"You have {availableHours} hours available.");
Console.WriteLine($"Application: {AppName}");
Console.WriteLine($"Maximum tasks allowed per day: {MaxTasks}");

// ---------- Step 3: Conditions ----------
if (taskGoal > MaxTasks)
{
    Console.WriteLine($"Your goal is above the maximum of {MaxTasks}. Be realistic!");
}

int completedTasks = ReadInt("How many tasks have you completed today? ");

if (completedTasks == 0)
{
    Console.WriteLine("Time to get started!");
}
else if (completedTasks < 3)
{
    Console.WriteLine("Good start. Keep going!");
}
else
{
    Console.WriteLine("Great progress!");
}

// ---------- Steps 4, 5 and 6: Main application loop ----------
List<string> tasks = new List<string>();
bool running = true;

ShowTitle();

while (running)
{
    ShowMenu();
    int option = ReadInt("Choose: ");

    switch (option)
    {
        case 1:
            AddTask(tasks);
            break;

        case 2:
            ShowTasks(tasks);
            break;

        case 3:
            RemoveTask(tasks);
            break;

        case 4:
            ShowTaskCount(tasks);
            break;

        case 0:
            running = false;
            ShowMessage("Goodbye!");
            break;

        default:
            ShowMessage("Invalid option.");
            break;
    }
}

// ======================= Methods (Step 5) =======================

void ShowTitle()
{
    Console.WriteLine();
    Console.WriteLine(AppName);
    Console.WriteLine("====================");
}

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("1. Add task");
    Console.WriteLine("2. View tasks");
    Console.WriteLine("3. Remove task");
    Console.WriteLine("4. Show task count");
    Console.WriteLine("0. Exit");
}

void ShowMessage(string message)
{
    Console.WriteLine($"[INFO] {message}");
}

int GetTaskCount(List<string> taskList)
{
    return taskList.Count;
}

void ShowTasks(List<string> taskList)
{
    if (taskList.Count == 0)
    {
        Console.WriteLine("No tasks available.");
        return;
    }

    for (int i = 0; i < taskList.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {taskList[i]}");
    }
}

void AddTask(List<string> taskList)
{
    if (taskList.Count >= MaxTasks)
    {
        ShowMessage("Maximum number of tasks reached.");
        return;
    }

    string title = ReadText("Enter task: ");
    TaskPriority priority = ReadPriority();

    switch (priority)
    {
        case TaskPriority.Low:
            Console.WriteLine("This task can wait.");
            break;

        case TaskPriority.Medium:
            Console.WriteLine("Try to finish this task today.");
            break;

        case TaskPriority.High:
            Console.WriteLine("Prioritize this task.");
            break;
    }

    taskList.Add($"{title} [{priority}]");
    ShowMessage("Task added.");
}

void RemoveTask(List<string> taskList)
{
    if (taskList.Count == 0)
    {
        Console.WriteLine("No tasks to remove.");
        return;
    }

    ShowTasks(taskList);
    int number = ReadInt("Task to remove: ");

    // The user sees numbers starting at 1, but list indexes start at 0.
    if (number < 1 || number > taskList.Count)
    {
        ShowMessage("Invalid task number.");
        return;
    }

    taskList.RemoveAt(number - 1);
    ShowMessage("Task removed.");
}

void ShowTaskCount(List<string> taskList)
{
    int count = GetTaskCount(taskList);
    Console.WriteLine($"You currently have {count} tasks.");
    Console.WriteLine($"Maximum allowed: {MaxTasks}");
    Console.WriteLine($"Remaining capacity: {MaxTasks - count}");
}

// ======================= Safe input helpers =======================
// TryParse is used instead of Parse so bad input never crashes the program.

string ReadText(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine() ?? "";
}

int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            return value;
        }
        Console.WriteLine("Please enter a whole number.");
    }
}

double ReadDouble(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        // Accept both "2.5" and "2,5" regardless of the computer's regional settings.
        string text = (Console.ReadLine() ?? "").Replace(',', '.');
        if (double.TryParse(text, System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out double value))
        {
            return value;
        }
        Console.WriteLine("Please enter a number.");
    }
}

bool ReadYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string answer = (Console.ReadLine() ?? "").Trim().ToLower();
        if (answer == "y" || answer == "yes") return true;
        if (answer == "n" || answer == "no") return false;
        Console.WriteLine("Please answer y or n.");
    }
}

TaskPriority ReadPriority()
{
    while (true)
    {
        int choice = ReadInt("Priority (1 = Low, 2 = Medium, 3 = High): ");
        if (choice >= 1 && choice <= 3)
        {
            return (TaskPriority)(choice - 1);
        }
        Console.WriteLine("Please choose 1, 2 or 3.");
    }
}

// ======================= Types (Step 6) =======================
// Type declarations must go after all top-level statements.

enum TaskPriority
{
    Low,
    Medium,
    High
}
