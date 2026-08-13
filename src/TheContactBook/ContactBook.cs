namespace TheContactBook;

public class ContactBook
{
    public const string YES = "Y";
    public const string NO = "N";

    public readonly string[] YES_NO = new string[] { YES, NO };

    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
    private const string GOTO_PAGE = "G";
    private const string PAGE_SIZE = "S";
    private const string CREATE_CONTACT = "C";
    private const string REVIEW_CONTACT = "R";
    private const string UPDATE_CONTACT = "U";
    private const string DELETE_CONTACT = "D";
    private const string FIND_CONTACTS = "F";
    private const string ORDER_CONTACTS = "O";
    private const string DEDUPLICATE_CONTACTS = "M";
    private const string EXIT = "X";

    public readonly string[] COMANDS = new string[]
    {
        NEXT_PAGE, PREV_PAGE, GOTO_PAGE, PAGE_SIZE,
        CREATE_CONTACT, REVIEW_CONTACT, UPDATE_CONTACT, DELETE_CONTACT,
        FIND_CONTACTS, ORDER_CONTACTS, DEDUPLICATE_CONTACTS, EXIT
    };
    
    private List<Contact> allContacts;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
    }

    public void Start()
    {
        ShowWelcomeScreen();

        string input;
        do
        {
            

            do
            {
                ShowContacts();
                ShowInputOptions();
                input = GetInput();
                
            }
            while (!IsValidInput(input));

            ProcessInput(input);
        }
        while(!ConfirmExit());
        
        ShowExitScreen();
    }

     private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welcome to the Contact Book!");
        PressEnterToContinue();
    }

    private void ShowContacts()
    {
        if (allContacts.Count == 0)
        {
            Console.WriteLine("No contacts found.");
        }
        else
        {
            int indexCol = -allContacts.Count.ToString().Length;
            int fnameCol = -allContacts.Max(c => c.GetFname()?.Length ?? 0);
            int lnameCol = -allContacts.Max(c => c.GetLname()?.Length ?? 0);
            int phoneCol = -allContacts.Max(c => c.GetPhone()?.Length ?? 0);
            int emailCol = -allContacts.Max(c => c.GetEmail()?.Length ?? 0);

            //header: had to do it in another format

            Console.WriteLine($"{"#".PadRight(-indexCol)} " +
            $"{"   First Name  ".PadRight(-fnameCol)} " +
            $"{"Last Name  ".PadRight(-lnameCol)} " +
            $"{"   Phone  ".PadRight(-phoneCol)} " +
            $"{"            Email  ".PadRight(-emailCol)}");

            Console.WriteLine(new string('-', 80));

            int n = allContacts.Count;
            int page = 1;
            int size = 10;
            int pageCount = (int)  Math.Max(1, Math.Ceiling(n/ (double) size));
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);
           
            

            for(int i = s; i < e; i++)
            {
                //contacts: had to do it in another format

                var contact = allContacts[i];
                Console.WriteLine($"{(i + 1).ToString().PadRight(-indexCol)}. " +
                $"   {contact.GetFname()?.PadRight(-fnameCol)}    " +
                $"   {contact.GetLname()?.PadRight(-lnameCol)}    " +
                $"   {contact.GetPhone()?.PadRight(-phoneCol)}    " +
                $"    {contact.GetEmail()?.PadRight(-emailCol)}   ");
            }
            Console.WriteLine();
            Console.WriteLine($"Page {page} of {pageCount} ({s}-{e} of {n})");

        }
    }

    private void ShowInputOptions()
    {
        string inputOptions = "" 
        +$"{NEXT_PAGE} - Next Page, | [{CREATE_CONTACT}] Create Contact | [{DELETE_CONTACT}] Delete Contact,  | [{DEDUPLICATE_CONTACTS}] Deduplicate Contacts\n"
        +$"{GOTO_PAGE} - Go to Page, | [{UPDATE_CONTACT}] Update Contact | [{ORDER_CONTACTS}] Order Contacts | [{PAGE_SIZE            }] Change Page Size\n" 
        +$"{PREV_PAGE} - Previous Page, | [{REVIEW_CONTACT}] Review Contact | [{FIND_CONTACTS}] Find Contacts | [{EXIT                }] Exit\n"
        +$"\n> ";


        Console.WriteLine();
        Console.Write(inputOptions);
        
    }

    private string GetInput()
    {
        return Console.ReadLine()!.ToUpper();
    }

    private bool IsValidInput(string input)
    {
        
        if(!COMANDS.Contains(input))
        {
            Console.WriteLine("Invalid input");
            PressEnterToContinue();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ProcessInput(string input)
    {
        switch(input)
        {
            case NEXT_PAGE:NextPage();
                break;
            case PREV_PAGE:PrevPage();
                break;
            case GOTO_PAGE:GotoPage();
                break; 
            case PAGE_SIZE:PageSize();
                break;
            case CREATE_CONTACT:CreateContact();
                break;
            case REVIEW_CONTACT:ReviewContact();
                break;
            case UPDATE_CONTACT:UpdateContact();
                break;
            case DELETE_CONTACT:DeleteContact();
                break;  
            case FIND_CONTACTS:FindContacts();
                break;
            case ORDER_CONTACTS:OrderContacts();
                break;
            case DEDUPLICATE_CONTACTS:DeduplicateContacts();
                break;
            case EXIT:Exit();
                break;
            default: break;
        }
    }

    private void NextPage()
    {
        Console.WriteLine("Next Page");
    }

    private void PrevPage()
    {
        Console.WriteLine("Previous Page");
    }

    private void GotoPage()
    {
        Console.WriteLine("Go to Page");
    }

    private void PageSize()
    {
        Console.WriteLine("Change Page Size");
    }

    private void CreateContact()
    {
        Console.WriteLine("Create Contact");
    }

    private void ReviewContact()
    {
        Console.WriteLine("Review Contact");
    }

    private void UpdateContact()
    {
        Console.WriteLine("Update Contact");
    }

    private void DeleteContact()
    {
        Console.WriteLine("Delete Contact");
    }

    private void FindContacts()
    {
        Console.WriteLine("Find Contacts");
    }

    private void OrderContacts()
    {
        Console.WriteLine("Order Contacts");
    }

    private void DeduplicateContacts()
    {
        Console.WriteLine("Deduplicate Contacts");
    }

    private void Exit()
    {
        Console.WriteLine("Exit");
    }

    private string GetOptions(string prompt, string [] validOptions, string defaultOption)
    {
        string options = string.Join("/", validOptions);
        Console.Write(prompt + $"[{options}] ({defaultOption}) ");
        string option = Console.ReadLine()!.ToUpper();
        if(string.IsNullOrWhiteSpace(option))
        {
            option = defaultOption;
        }
        while(!validOptions.Contains(option))
        {
            Console.WriteLine("Error: Invalid Option. Please try again.");
            Console.Write(prompt + $"[{options}] ({defaultOption}) ");
            option = Console.ReadLine()!;
            if(string.IsNullOrWhiteSpace(option))
            {
                option = defaultOption;
            }
        }
        return option;
        
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOptions(prompt, YES_NO, defaultOption) == YES;
    }

    private bool ConfirmExit()
    {
        return Confirm("Do you want to exit?", NO);
    }
    private void ShowExitScreen()
    {
        Console.Clear();
        Console. WriteLine("Thank you for using the Contact Book!");
    }

    private void PressEnterToContinue()
    {
        Console.WriteLine("Press Enter to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter);

    }   


}