namespace NPokemon
{
    public class Attack
    {
    // VALUES
    private string Name;
    private string Type;
    private string Cat;
    private int Power;
    private int Acc;
    private int Pp;
    private int BasePp;

    // CONSTRUCTOR
    public Attack(string name, string type, string cat, int power, int acc, int pp)
    {
        Name = name;
        Type = type;
        Cat = cat;
        Power = power;
        Acc = acc;
        Pp = pp;
        BasePp = pp;
    }

    // CHANGERS
    public void ChangePp(int x)
    {
        Pp += x;
        if (Pp > BasePp)
        {
            Pp = BasePp;
        }
        else if (Pp < 0)
        {
            Pp = 0;
        }
    }

    public void ChangeBasePp(int x){BasePp += x;}
    public void UseAttack()
	{
        if (Pp > 0)
        {
            Pp -= 1;
        }
    }

    public void ResetPp(){Pp = BasePp;}

    // GETTERS
    public string GetName(){return Name;}
    public new string GetType(){return Type;}

    public string GetCat(){return Cat;}
    public int GetPower(){return Power;}
    public int GetAcc(){return Acc;}
    public int GetPp(){return Pp;}
    public int GetMaxPp(){return BasePp;}
    }
}
