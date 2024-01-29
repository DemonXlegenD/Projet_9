namespace Csharp_Tpt
{
	public enum AITrainerParameter{
	   Child, // Random 
	   Newbie, // Best Move, doesnt care if it's an attack or not
	   Jarod,
	   Magnus,
	   Hacker, // Knows all 
	   None
	}

    public enum States{
        LowHealth,
        HightHealth,
        Other
    }
    public class AITrainer{
        States STATE = States.Other;
    }
}