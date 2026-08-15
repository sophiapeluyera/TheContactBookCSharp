namespace TheContactBook;

public class Contact : IEquatable<Contact>
{
    private string fname = default!;
    private string lname = default!;
    private string phone = default!;
    private string email = default!;

    public Contact(string fname, string lname, string phone, string email)
    {
        SetFname(fname);
        SetLname(lname);
        SetPhone(phone);
        SetEmail(email);
    }

    public Contact()
    {
    }

    public string GetFname()
    {
        return fname;
    }   

    public string GetLname()
    {
        return lname;
    }

    public string GetPhone()
    {
        return phone;
    }

    public string GetEmail()
    {
        return email;
    }

    public void SetFname(string fname)
    {
        this.fname = fname;
    }

    public void SetLname(string lname)
    {
        this.lname = lname;
    }

    public void SetPhone(string phone)
    {
        this.phone = phone;
    }

    public void SetEmail(string email)
    {
        this.email = email;
    }

    public override string ToString()
    {
        return $"Contact: {fname} {lname}, Phone: {phone}, Email: {email}";
    }

    public bool Equals(Contact? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return fname == other.fname && lname == other.lname && phone == other.phone && email == other.email;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Contact);
    }

    public static bool operator ==(Contact? x, Contact? y)
    {
        if (x is null && y is null)
            return true;
        if (x is null || y is null)
            return false;
        return x.Equals(y);
    }

    public static bool operator !=(Contact? x, Contact? y)
    {
        return !(x == y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(fname, lname, phone, email);
    }
}