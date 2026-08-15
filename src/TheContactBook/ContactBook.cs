namespace TheContactBook;

using System.Runtime.CompilerServices;
using static ContactComparer;
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
    
    private List<Contact> filteredContacts;
    private List<Contact> allContacts;
    private int page;
    private int size;
    private bool isExit;

    public ContactBook(List<Contact> contacts = null!)
    {
        allContacts = (contacts == null) ? new List<Contact>() : contacts;
        filteredContacts = allContacts;
        page = 1;
        size = 10;
        isExit = false;
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
        Console.Clear();
        ShowContacts(filteredContacts, page , size);
    }

    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found.");
        }
        else
        {
            int indexCol = -contacts.Count.ToString().Length;
            int fnameCol = -contacts.Max(c => c.GetFname()?.Length ?? 0);
            int lnameCol = -contacts.Max(c => c.GetLname()?.Length ?? 0);
            int phoneCol = -contacts.Max(c => c.GetPhone()?.Length ?? 0);
            int emailCol = -contacts.Max(c => c.GetEmail()?.Length ?? 0);

            //header: had to do it in another format

            Console.WriteLine($"{"#".PadRight(-indexCol)} " +
            $"{"   First Name  ".PadRight(-fnameCol)} " +
            $"{"Last Name  ".PadRight(-lnameCol)} " +
            $"{"   Phone  ".PadRight(-phoneCol)} " +
            $"{"            Email  ".PadRight(-emailCol)}");

            Console.WriteLine(new string('-', 80));

            int n = contacts.Count;
            int pageCount = PageCount(contacts, size);
            int s = Math.Clamp((page - 1) * size, 0, n);
            int e = Math.Clamp(s + size, 0, n);



            for (int i = s; i < e; i++)
            {
                //contacts: had to do it in another format

                var contact = contacts[i];
                Console.WriteLine($"{(i + 1).ToString().PadRight(-indexCol)}. " +
                $"   {contact.GetFname()?.PadRight(-fnameCol)}    " +
                $"   {contact.GetLname()?.PadRight(-lnameCol)}    " +
                $"   {contact.GetPhone()?.PadRight(-phoneCol)}    " +
                $"    {contact.GetEmail()?.PadRight(-emailCol)}   ");
            }

            for(int i = 0; i < size - (e - s); i++)
            {
                Console.WriteLine();
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
        NextPage(filteredContacts, ref page, size);
        
    }

    private void NextPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page + 1, 1, PageCount(contacts, size));
    }
    private void PrevPage()
    {
        PrevPage(filteredContacts, ref page, size);
        
    }

    private void PrevPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page - 1, 1, PageCount(contacts, size));
    }

    private void GotoPage()
    {
        GotoPage(filteredContacts, ref page, size);
        
    }

    private void GotoPage(List<Contact> contacts, ref int page, int size)
    {
        page = GetInt("Enter page number: ", 1, PageCount(contacts, size));
    }

    private void PageSize()
    {
        PageSize(ref page , ref size);
    }

    private void PageSize( ref int page, ref int size)
    {
        int max = Console.WindowHeight - 10;
        size = GetInt("Enter page size: ", 1, max);
        page =1;
    }

    private void CreateContact()
    {
        Console.Clear();
        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine("Create Contact");
        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine();
        Console.Write("Enter First Name:");
        string fname = Console.ReadLine()!; 
        Console.Write("Enter Last Name:");
        string lname = Console.ReadLine()!; 
        Console.Write("Enter Phone:");
        string phone = Console.ReadLine()!;
        Console.Write("Enter email:");
        string email = Console.ReadLine()!;

        Console.WriteLine();

        if (Confirm("Do you want to save this contact?", YES))
        {
            Contact c = new Contact(fname, lname, phone, email);
            filteredContacts.Add(c);
            page = PageCount(filteredContacts, size);

            Console.WriteLine("Contact created successfully.");
        }
        else
        {
            Console.WriteLine("Contact not created.");
        }

        Console.WriteLine();
        PressEnterToContinue();

    }

    private void ReviewContact()
    {
        int index = GetInt("Enter index: ", 1, filteredContacts.Count) - 1;

        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine("Review Contact");
        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine();


        Console.Clear();

        ReviewContact(index);
        PressEnterToContinue();
    }

    private void ReviewContact(int index)
    {
        Contact c = filteredContacts[index];

        Console.WriteLine($"Enter First Name: {c.GetFname()}");
        Console.WriteLine($"Enter Last Name: {c.GetLname()}");
        Console.WriteLine($"Enter Phone: {c.GetPhone()}");
        Console.WriteLine($"Enter email: {c.GetEmail()}");

        Console.WriteLine();    
    }

     private void UpdateContact()
    {
        int index = GetInt("Enter index: ", 1, filteredContacts.Count) - 1;

        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine("Update Contact");
        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine();

        Console.Clear();

        UpdateContact(index);
        Console.WriteLine();
        PressEnterToContinue();
    }

    private void UpdateContact(int index)
    {
        Contact c = filteredContacts[index];

        string fname =c.GetFname();
        string lname = c.GetLname();
        string phone = c.GetPhone();
        string email = c.GetEmail();

        ReviewContact(index);

        if (Confirm("Do you want to edit the first name?", NO))
        {

            Console.Write("Enter first name: ");
            fname = Console.ReadLine()!;

        }

        
        if (Confirm("Do you want to edit the last name?", NO))
        {

            Console.Write("Enter last name: ");
            lname = Console.ReadLine()!;

        }

        
        if (Confirm("Do you want to edit the phone number?", NO))
        {

            Console.Write("Enter phone number: ");
            phone = Console.ReadLine()!;

        }

        
        if (Confirm("Do you want to edit the email?", NO))
        {

            Console.Write("Enter email: ");
            email = Console.ReadLine()!;

        }

        Console.WriteLine();

        if (Confirm("Do you want to update this contact?", NO))
        {
            c.SetFname(fname);
            c.SetLname(lname);
            c.SetPhone(phone);
            c.SetEmail(email);

            Console.WriteLine("Contact updated successfully.");
        }
        else
        {
            Console.WriteLine("Contact not updated.");
        }
  
    }

    private void DeleteContact()
    {
        int index = GetInt("Enter index: ", 1, filteredContacts.Count) - 1;

        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine("Delete Contact");
        Console.WriteLine(new string ('#', 80) );
        Console.WriteLine();

        Console.Clear();

        DeleteContact(index);

        Console.WriteLine();
        PressEnterToContinue();
    }

    private void DeleteContact(int index)
    {
        Contact c = filteredContacts[index];
        ReviewContact(index);

        Console.WriteLine();

        if (Confirm("Do you want to delete this contact?", NO))
        {
            filteredContacts.Remove(c);

            Console.WriteLine("Contact deleted successfully.");
        }
        else
        {
            Console.WriteLine("Contact not deleted.");
        }
  
    }


    private void FindContacts()
    {
        Console.WriteLine("Enter search term (Clear): ");
        string searchTerm = Console.ReadLine()!.ToLower();

        Console.WriteLine();

        if (Confirm("Do you want to search contacts?", YES))
        {
            filteredContacts = allContacts.FindAll(c =>
            (c.GetFname()+c.GetLname()+c.GetPhone()+c.GetEmail()).ToLower().Contains(searchTerm));
            page = 1;

            Console.WriteLine("Contact/Contacts found.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }

        PressEnterToContinue();
    }

    private void OrderContacts()
    {
        SortType[] sortTypes = new SortType[]
        {
            SortType.Fname, SortType.Lname, SortType.Phone, SortType.Email
        };

        int index = GetInt ("Sort contacts by [0] Fname [1] Lname [2] Phone [3] Email", 0, 3);

        ContactComparer ccp = new ContactComparer(sortTypes[index]);
        allContacts.Sort(ccp);
        filteredContacts.Sort(ccp);    
    }

    private void DeduplicateContacts()
    {
        List<List<Contact>> duplicateGroups = ContactMerger.FindDuplicates(allContacts);
        List<Contact> temp = new List<Contact>();

        foreach(var group in duplicateGroups)
        {
            if(group.Count> 1)
            {
                Console.Clear();
                Console.WriteLine(new string('#', 80));
                Console.WriteLine("Duplicate Contacts:");
                Console.WriteLine(new string('#', 80));
                Console.WriteLine();
                ShowContacts(group, 1, group.Count);
                int fnameIndex =GetInt("Enter first name index", 1, group.Count)-1;
                int lnameIndex =GetInt("Enter first name index", 1, group.Count)-1;
                int phoneIndex =GetInt("Enter phone index", 1, group.Count)-1;
                int emailIndex =GetInt("Enter email index", 1, group.Count)-1;

                Contact merged = new Contact();
                merged.SetFname(group[fnameIndex].GetFname());
                merged.SetLname(group[lnameIndex].GetLname());
                merged.SetPhone(group[phoneIndex].GetPhone());
                merged.SetEmail(group[emailIndex].GetEmail());
                
                Console.Clear();
                Console.WriteLine(new string('#', 80));
                Console.WriteLine("Merged Contact:");
                Console.WriteLine(new string('#', 80));
                Console.WriteLine();

                List<Contact> mergedLS = new List<Contact> {merged};
                ShowContacts(mergedLS, 1, 1);
                Console.WriteLine();

                if(Confirm("Do you want to merge these contacts?", NO))
                {
                    temp.Add(merged);
                    Console.WriteLine("Operation successful.");
                }
                else
                {
                    temp.AddRange(group);
                    Console.WriteLine("Operation cancelled.");
            
                }

                PressEnterToContinue();
            }
            else
            {
                temp.AddRange(group);
            }
           Console.WriteLine();
            
        }

        if(Confirm("Do you want to apply all changes to the contacts?", NO))
        {
            allContacts = filteredContacts = temp;
            Console.WriteLine("Contacts deduplicated.");
        }
        else
        {
            Console.WriteLine("Operation cancelled.");
        }
        Console.WriteLine();
        PressEnterToContinue();

    }

    private void Exit()
    {
        isExit = true;
    }
    
    private int GetInt(string prompt, int min, int max)
    {
        string options = $"{min}-{max}";

        Console.Write(prompt + $"[{options}] ");
        string answer = Console.ReadLine()!.ToUpper();
        int value;

        
        while(!int.TryParse(answer, out value) || value < min || value > max)
        {
            Console.WriteLine("Error: Invalid Option. Please try again.");
            Console.Write(prompt + $"[{options}] ");
            answer  = Console.ReadLine()!;

        }
        return value;
    }
    private string GetOption(string prompt, string [] validOptions, string defaultOption)
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
        return GetOption(prompt, YES_NO, defaultOption) == YES;
    }

    private static int PageCount(List<Contact> contacts , int size)
    {
        return (int)Math.Max(1, Math.Ceiling(contacts.Count / (double)size));
    }

    private bool ConfirmExit()
    {
        return (isExit) ? isExit =  Confirm("Do you want to exit?", NO) : false;
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