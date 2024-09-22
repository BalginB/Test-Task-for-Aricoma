using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GreenTechFolderAutomation
{
    class Program
    {
        // A dictionary to hold folder templates
        static Dictionary<string, List<string>> templates = new Dictionary<string, List<string>>()
        {
            {"basic", new List<string> { "Documents", "Source Code", "Reports" }},
            {"standard", new List<string> { "Design", "Test Results", "Resources" }},
            {"advanced", new List<string> { "Data", "Code", "Analysis" }},
            {"custom", new List<string>()} // User can modify this template
        };

        // A dictionary to keep track of which template each project is using
        static Dictionary<string, string> projectTemplateMapping = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=======================================");
            Console.WriteLine("    GreenTech Solutions Folder Automation");
            Console.WriteLine("=======================================\n");
            Console.ResetColor();

            // Step 1: Select a Drive
            string selectedDrive = SelectDrive();

            // Step 2: Folder Navigation and Project Creation
            FolderNavigation(selectedDrive);
        }


        // Ensures user input is within a valid integer range
        static int GetValidatedInput(int max)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                // Check for invalid input (null, empty, non-numeric, or out of range)
                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int selected) || selected < 1 || selected > max)
                {
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {max}.");
                    continue;
                }

                return selected;
            }
        }


        // Ensures non-empty string input from the user
        static string GetValidatedStringInput()
        {
            while (true)
            {
                string? input = Console.ReadLine();

                // Check for empty input
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid string.");
                    continue;
                }

                return input;
            }
        }


        // Allows the user to select a drive for folder navigation and project creation
        static string SelectDrive()
        {
            // Get a list of all available drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select a drive:");
            Console.ResetColor();
            // Display all drives
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {drives[i].Name}");
            }
            // Get validated drive selection from user
            Func<int, int> driveIndex = GetValidatedInput;
            return drives[driveIndex(drives.Length) - 1].Name;
        }

        // Manages folder navigation and project creation
        static void FolderNavigation(string currentPath)
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nCurrent Directory: {currentPath}");
                Console.ResetColor();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Options:");
                Console.WriteLine("1. Create a Project");
                Console.WriteLine("2. Navigate to Subfolder");
                Console.WriteLine("3. Go to Parent Folder");
                Console.WriteLine("4. Update Templates");
                Console.WriteLine("5. Exit");
                Console.WriteLine("---------------------------------------");


                Func<int, int> choiceIndex = GetValidatedInput;
                // Handle user menu selection
                switch (choiceIndex(5))
                {
                    case 1:
                        CreateProject(currentPath);
                        break;
                    case 2:
                        NavigateToSubfolder(currentPath);
                        break;
                    case 3:
                        // Navigate back to the parent folder, ensuring we're not at the root
                        if (currentPath == Path.GetPathRoot(currentPath))
                        {
                            Console.WriteLine("You are already in the main (parent) folder.");
                        }
                        else
                        {
                            currentPath = Directory.GetParent(currentPath)?.FullName ?? currentPath;
                        }
                        break;
                    case 4:
                        UpdateTemplates();
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        // Allows the user to navigate to a subfolder within the current directory
        static void NavigateToSubfolder(string currentPath)
        {
            string[] subfolders = Directory.GetDirectories(currentPath);
            if (subfolders.Length == 0)
            {
                Console.WriteLine("No subfolders available.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select a subfolder by number:");
            Console.ResetColor();

            // List all available subfolders
            for (int i = 0; i < subfolders.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(subfolders[i])}");
            }

            // Handle subfolder selection
            Func<int, int> subfolderChoice = GetValidatedInput;
            int choice = subfolderChoice(subfolders.Length) - 1;
            if (choice >= 0 && choice < subfolders.Length)
            {
                string selectedSubfolder = subfolders[choice];
                FolderNavigation(selectedSubfolder);
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        // Handles the process of creating a new project, including folder setup
        static void CreateProject(string currentPath)
        {
            Console.WriteLine("Enter project name:");
            Func<string> projectName = GetValidatedStringInput;

            // Combine current path with project name to form project directory
            string projectPath = Path.Combine(currentPath, projectName());

            // Check if the project directory already exists
            if (Directory.Exists(projectPath))
            {
                Console.WriteLine("Project already exists.");
                return;
            }
            
            // User selects a template for the project
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select a template:\n1. basic\n2. standard\n3. advanced\n4. custom (Modify)");
            Console.ResetColor();

            int templateChoice = GetValidatedInput(4);
            List<string> selectedTemplate;
            string templateName;

            if (templateChoice == 4)
            {
                // Modify custom template if selected
                ModifyCustomTemplate();
                selectedTemplate = templates["custom"];
                templateName = "custom";
            }
            else
            {
                // Map the user's template choice to one of the predefined templates
                templateName = templateChoice switch
                {
                    1 => "basic",
                    2 => "standard",
                    3 => "advanced",
                    _ => "basic"
                };

                selectedTemplate = templates[templateName];
            }
            // Create project folders based on the selected template
            CreateFolders(projectPath, selectedTemplate);
            projectTemplateMapping[projectPath] = templateName;

        }

        // Allows the user to modify the custom template by adding folders
        static void ModifyCustomTemplate()
        {
            Console.WriteLine("Modifying Custom Template:");
            templates["custom"].Clear(); // Reset the custom template

            Console.WriteLine("Enter folder names for Custom Template (type 'done' when finished):");
            CustomizeTemplate("custom");// Populate custom template with new folder names
        }
        
        // Adds folders to a template based on user input
        static void CustomizeTemplate(string template)
        {
            Func<string> folderName = GetValidatedStringInput;
            while (true)
            {
                string name = folderName();
                if (name.ToLower() == "done")
                {
                    break;
                }
                templates[template].Add(name);// Add folder to template
                Console.WriteLine($"Added '{name}' to template '{template}'.");
            }
        }

        // Creates directories for he project based on the selected template
        static void CreateFolders(string projectPath, List<string> templates)
        {
            Directory.CreateDirectory(projectPath); // Create project root folder

            // Create each subfolder as defined in the selected template
            foreach (string folder in templates)
            {
                string folderPath = Path.Combine(projectPath, folder);
                Directory.CreateDirectory(folderPath);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Project folders created successfully.");
            Console.ResetColor();
        }
        
        // Allows the user to update any template, including custom templates
        static void UpdateTemplates()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Select the template to update:\n1. basic\n2. standard\n3. advanced\n4. custom");
            Console.ResetColor();

            // Template selection for updating
            string templateName = GetValidatedInput(4) switch
            {
                1 => "basic",
                2 => "standard",
                3 => "advanced",
                4 => "custom",
                _ => "basic"
            };
            // Modify the selected template
            if (templateName == "custom")
            {
                ModifyCustomTemplate();
            }
            else
            {
                Console.WriteLine($"Modifying {templateName} Template:");
                templates[templateName].Clear();// Reset the selected template

                Console.WriteLine($"Enter folder names for {templateName} Template (type 'done' when finished):");
                CustomizeTemplate(templateName); // Populate wth new folder names
            }

            // Update all projects using the modified template
            DynamicTemplateUpdate(templateName);
        }

        // Dynamically updates all projects using a specific template
        static void DynamicTemplateUpdate(string templateName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Updating all projects using the {templateName} template...");
            Console.ResetColor();

            // Loop through all projects using the updated template
            foreach (var project in projectTemplateMapping.Where(p => p.Value == templateName))
            {
                string projectPath = project.Key;
                List<string> templateFolders = templates[templateName];

                // Get all existing folders in the project
                var existingFolders = Directory.GetDirectories(projectPath);

                // Check for folders to remove
                foreach (var folder in existingFolders)
                {
                    string folderName = Path.GetFileName(folder);

                    // If the folder doesn't exist in the new template, prompt the user to delete it
                    if (!templateFolders.Contains(folderName))
                    {
                        if (Directory.GetFiles(folder).Length > 0 || Directory.GetDirectories(folder).Length > 0)
                        {
                            Func<string> userResponse = GetValidatedStringInput;
                            Console.WriteLine($"Folder '{folderName}' is not empty. Do you want to delete it? (yes/no)");
                            if (userResponse().ToLower() == "yes")
                            {
                                Directory.Delete(folder, true); // Recursively delete folder and its contents
                                Console.WriteLine($"Deleted folder: {folderName}");
                            }
                            else
                            {
                                Console.WriteLine("Folder deletion canceled.");
                            }
                        }
                        else
                        {
                            Directory.Delete(folder, true); // Delete empty folders
                            Console.WriteLine($"Deleted empty folder: {folderName}");
                        }
                    }
                }

                // Add missing folders from the new template
                foreach (string folder in templateFolders)
                {
                    string folderPath = Path.Combine(projectPath, folder);
                    if (!Directory.Exists(folderPath))
                    { 
                        Directory.CreateDirectory(folderPath); // Create missing folders
                        Console.WriteLine($"Added folder '{folder}' to project: {projectPath}");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"All projects using the {templateName} template have been updated.");
            Console.ResetColor();
        }
    }
}
