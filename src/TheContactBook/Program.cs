namespace TheContactBook;

    public class Program
{
    public static void Main()
{
;
    var cb = new ContactBook(new List<Contact> (ContactSeed.Contacts));
    cb.Start();
}

}
