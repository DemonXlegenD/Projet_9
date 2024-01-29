namespace Csharp_Tpt
{
	public enum AITrainerParameter{
	   Child, // Random 
	   Newbie, // Best Move, doesnt care if it's an attack or not
	   Jarod, // Best Move, care if it's an attack, if low heal if he can
	   Magnus, // Utilise toutes les données que l'on peut normalement avoir et regarde
	   Hacker, // Knows all , does the best thing regarding all the things he knows
	   None // ?????
	}

    public class AITrainer{
        States STATE = States.Other;
    }
}