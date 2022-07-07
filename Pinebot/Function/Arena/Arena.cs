namespace PineBot.Function.Arena;

public class Arena : Addin , IAddin
{
    public List<Player> players = new List<Player>();
    public uint round = 0;
    public bool isMatch = false;

    public string? OnMessage(object? sender, EventArgs e)
    {
        
    }

    public Func<string?, object?, EventArgs> GetResponseFunction()
    {
        return this.OnMessage;
    }
}

public class Player
{
    private int no;
    private int tempScore;
    private string songName;
    public string name;
    public uint point;

    public Player(string playerName, string choiseSoneName, int no)
    {
        name = playerName;
        songName = choiseSoneName; 
        tempScore = -1;
        point = 0;
        no = this.no;
    }
}

