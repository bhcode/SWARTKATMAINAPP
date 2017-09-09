
public class User
{
    int id;
    string name;
    string email;
    string password;
    string remember_token; //optional variable, only used when taking whole users directly from the server
    string created_at; //optional variable, only used when taking whole users directly from the server
    string updated_at; //optional variable, only used when taking whole users directly from the server

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }

        set
        {
            email = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }

    public string Remember_token
    {
        get
        {
            return remember_token;
        }

        set
        {
            remember_token = value;
        }
    }

    public string Created_at
    {
        get
        {
            return created_at;
        }

        set
        {
            created_at = value;
        }
    }

    public string Updated_at
    {
        get
        {
            return updated_at;
        }

        set
        {
            updated_at = value;
        }
    }

    public User() { }
}