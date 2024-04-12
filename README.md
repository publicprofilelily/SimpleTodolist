# SimpleTodolist

## Overview
This TodoList application helps users manage their tasks effectively. It is built in C# and provides functionalities to add, update, remove, and toggle task completions through a user-friendly console interface.

## Features
- Display a summary of tasks
- Add new tasks with due dates and project categorization
- Edit existing tasks including their completion status
- Sort tasks by date or project
- Save tasks to a file

## Prerequisites
- .NET SDK (recommended version 5.0 or higher)
- Any IDE that supports C# (e.g., Visual Studio, VSCode with C# extension)

## Installation
1. Clone this repository or download the source code:

## Project file hierarchy

#Namespace and Directory Organization
 
# The UI namespace contains everything related to user interaction, while the root TodoList namespace could contain the core logic and data models.

# Class Responsibilities
MainMenu and EditMenu Classes: These classes are part of the user interface but they also manage user inputs and transitions between different states of the application. They control the flow of the application, deciding what happens next based on user input.

# ITaskManager Interface: Abstracts the functionalities related to task management, like adding, editing, or deleting tasks. 

# IUserInterface Interface: Similarly, this interface abstracts the user interface interactions away from the actual logic of handling tasks. 
