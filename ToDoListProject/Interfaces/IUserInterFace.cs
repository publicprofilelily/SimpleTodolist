using System.Collections.Generic;
using System;

namespace TodoList
{
    public interface IUserInterface
    {
        void PrintHeader(string title);
        void PrintFooter();
        void SetConsoleColor(ConsoleColor color);
        string PromptInput(string promptMessage);
        void DisplayMessage(string message, ConsoleColor color = ConsoleColor.White);
        void ClearScreen();
        void WaitForAnyKey(string message = "Press any key to continue");
        void DisplayOptions(string[] options);
        string CollectTaskInfo();
        void DisplayWelcomeMessage();
        void PrintEmptyLine();
    }
}
