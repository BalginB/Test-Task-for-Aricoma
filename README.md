# GreenTech Folder Automation

## Overview

The **GreenTech Folder Automation** project is a simple console-based application that automates the creation and management of folder structures for various projects. It allows users to:
- Navigate their file system.
- Create projects based on predefined or custom folder templates.
- Update folder templates and automatically apply changes across existing projects.

## Features

- **Drive Selection**: Choose the drive on which to create and manage your projects.
- **Folder Navigation**: Navigate to subfolders, go back to the parent folder, and create projects in any desired directory.
- **Project Creation**: Set up a new project by selecting from predefined folder templates or creating custom templates.
- **Dynamic Template Updates**: Modify templates and apply changes across existing projects using those templates.
- **Custom Folder Structures**: Modify or create your own folder structures for specific needs.

## Folder Templates

The program includes the following predefined folder templates:
- **Basic**: `Documents`, `Source Code`, `Reports`
- **Standard**: `Design`, `Test Results`, `Resources`
- **Advanced**: `Data`, `Code`, `Analysis`
- **Custom**: You can modify this template with your own folder names.

## Resources

- Stackoverflow to fix bugs.
- ChatGPT to quickly find information and help with code.
- My personal notes in OneNote as a primary source
- My course PRO C#. For advanced as the main source of knowledge.
- Video of a random Indian man on YouTube for console design.

## Installation

1. **Clone the repository**:
    ```bash
    git clone https://github.com/BalginB/Test-Task-for-Aricoma
    ```

2. **Build the project**:
    ```bash
    dotnet build
    ```

3. **Run the project**:
    ```bash
    dotnet run
    ```

## How to Use

1. **Drive Selection**:
   After running the program, you'll be prompted to select a drive (e.g., `C:\` or `D:\`) where you want to navigate and create projects.

2. **Folder Navigation**:
   Once you've selected a drive, you can navigate through the folder structure, create projects, or modify templates.

3. **Project Creation**:
   Choose the `Create a Project` option to set up a new project. You will be prompted to provide a project name and select one of the predefined templates (`basic`, `standard`, `advanced`) or modify the `custom` template with your own folder structure.

4. **Template Update**:
   If you update any folder template, all projects using that template will have their folder structures dynamically updated.

5. **Template Update with no-empty folders**:
   If you want to update any of the templates and all projects created on this template, then if any of the folders contain files, the program will ask you for confirmation to delete/skip these folders.

## Example Usage

1. Select a drive:
    ```bash
    1. C:\
    2. D:\
    Select a drive: 1
    ```

2. Create a project:
    ```bash
    Current Directory: C:\
    ---------------------------------------
    Options:
    1. Create a Project
    2. Navigate to Subfolder
    3. Go to Parent Folder
    4. Update Templates
    5. Exit
    ---------------------------------------
    Select an option: 1
    Enter project name: MyNewProject
    Select a template:
    1. basic
    2. standard
    3. advanced
    4. custom (Modify)
    Select a template: 1
    Project folders created successfully.  
    ```

3. Navigate to a subfolder:
    ```bash
    Current Directory: C:\
    ---------------------------------------
    Options:
    1. Create a Project
    2. Navigate to Subfolder
    3. Go to Parent Folder
    4. Update Templates
    5. Exit
    ---------------------------------------
    Select an option: 2
    Select a subfolder by number:
    1. Project1
    2. Project2
    Select a subfolder: 1
    Navigated to C:\Project1
    ```
4. Go to the parent folder:
    ```bash
    Current Directory: C:\Project1
    ---------------------------------------
    Options:
    1. Create a Project
    2. Navigate to Subfolder
    3. Go to Parent Folder
    4. Update Templates
    5. Exit
    ---------------------------------------
    Select an option: 3
    Navigated back to C:\  
    ```
5. Update templates:
    ```bash
    Current Directory: C:\
    ---------------------------------------
    Options:
    1. Create a Project
    2. Navigate to Subfolder
    3. Go to Parent Folder
    4. Update Templates
    5. Exit
    ---------------------------------------
    Select an option: 4
    Select the template to update:
    1. basic
    2. standard
    3. advanced
    4. custom
    Select a template: 2
    Modifying standard Template:
    Enter folder names for standard Template (type 'done' when finished):
    Test Scripts
    Docs
    done
    All projects using the standard template have been updated.
    ```
6. Exit the program:
    ```bash
    Current Directory: C:\
    ---------------------------------------
    Options:
    1. Create a Project
    2. Navigate to Subfolder
    3. Go to Parent Folder
    4. Update Templates
    5. Exit
    ---------------------------------------
    Select an option: 5
    Exiting the program...
    ```

## Contributing

If you have suggestions for improvements, feel free to fork the repository and submit a pull request. You can also open an issue if you find any bugs.


