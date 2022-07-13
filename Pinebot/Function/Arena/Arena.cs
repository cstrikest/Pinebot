using PineBot.Message;


namespace PineBot.Function.Arena;

public delegate void F(object? sender, MessageEventMessage e);

public class Arena : Addin, IAddin
{
    public List<Player> players = new List<Player>();
    public int round = 1;
    public bool isMatch = false;

    public void OnMessage(object? sender, MessageEventMessage e)
    {
        if ((string?)e.J["raw_message"] == "-a")
        {
            Console.WriteLine("Arena." + e.J["sender"]["card"]);
        }
    }

    public EventHandler<MessageEventMessage> GetResponseFunction()
    {
        return OnMessage;
    }

    private void Reset()
    {
        players.Clear();
        round = 1;
        isMatch = false;
    }

    private string PrintScore()
    {
        string msg = string.Empty;
        foreach (var player in players)
        {
            msg += player.name + ": " + player.point;
        }

        return msg;
    }

    private string PrintCurrentSong()
    {
        return players[(int)round - 1].songName;
    }

    private string AddPlayer(string name, string songName)
    {
        if (!isMatch)
        {
            foreach (var player in players)
            {
                if (player.name == name)
                {
                    return "重复选曲。";
                }
            }

            players.Append(new Player(name, songName, players.Count));
            return string.Empty;
        }
        else
        {
            return "比赛途中无法加入";
        }
    }

    private string StartMatch()
    {
        if (!isMatch)
        {
            if (players.Count <= 2)
            {
                return "自个儿玩?";
            }
            else
            {
                isMatch = true;
                return "Arena 开始。";
            }
        }

        return "比赛已经开始";
    }

    private string Scoring(string playerName, string score)
    {
        foreach (var player in players)
        {
            if (playerName != player.name) return "Arena进行中，您没有参加。";
        }

        string msg = string.Empty;
        int formartScore = 0;

        try
        {
            formartScore = Convert.ToInt32(score);
        }
        catch
        {
            return "啥分？";
        }

        if (formartScore < 0) return "负分？求你重新打";

        if (isMatch)
        {
            bool isOver = true;
            foreach (var player in players)
            {
                if (player.name == playerName)
                {
                    if (player.tempScore != -1) return "别重复提交";
                    player.tempScore = formartScore;
                    Console.WriteLine("提交分数" + playerName + ":" + score);
                    msg = playerName + "的" + players[round - 1].songName + ": " + score;
                }
            }

            foreach (var player in players)
            {
                if (player.tempScore == -1) isOver = false;
            }

            var scores = new List<int>();
            if (isOver)
            {
                msg += "\n本轮结束，分数为\n";
            }
        }
    }
}

public class Player
{
    private int no;
    public int tempScore;
    public string songName;
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