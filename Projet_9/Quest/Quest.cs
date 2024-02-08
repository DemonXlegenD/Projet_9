using System;
using System.Collections.Generic;

namespace NQuest
{
    // CA TU TOUCHE PAS
    public delegate void TrainerDefeatedEventHandler(object sender, TrainerDefeatedEventArgs e, string trainerName);
    public delegate void PokemonCapturedEventHandler(object sender, PokemonCapturedEventArgs e);
    public delegate void PokemonCollectedEventHandler(object sender, PokemonCollectedEventArgs e);

    // CA NON PLUS
    public class TrainerDefeatedEventArgs : EventArgs 
    {
        public TrainerDefeatedEventArgs() { }
    }
    public class PokemonCapturedEventArgs : EventArgs 
    {
        public PokemonCapturedEventArgs() { }
    }
    public class PokemonCollectedEventArgs : EventArgs 
    {
        public PokemonCollectedEventArgs() { }
    }

    // CA TU TOUCHE MAIS PAS A TOUT
    public class QuestManager
    {
        // CA NON
        public event TrainerDefeatedEventHandler TrainerDefeatedEvent;
        public event PokemonCapturedEventHandler PokemonCapturedEvent;
        public event PokemonCollectedEventHandler PokemonCollectedEvent;

        // CA CEST LA LISTE DES QUETES EN FONCTION DE LEUR ETAT: DISPONIBLE, EN COURS, COMPLETE
        List<Quest> availableQuests;
        List<Quest> activeQuests;
        List<Quest> completedQuests;

        // LE INIT BASIQUE
        public QuestManager()
        {
            availableQuests = new List<Quest>();
            activeQuests = new List<Quest>();
            completedQuests = new List<Quest>();
        }

        // CREER UNE QUETE
       public void AddAvailableQuest(Quest quest)
        {
            availableQuests.Add(quest);
        }

        // DEMARRER LA QUETE
        public void StartQuest(Quest quest)
        {
            availableQuests.Remove(quest);
            activeQuests.Add(quest);
        }

        // RECUPERER LEVENEMENT JCROIS JE SAIS PLUS
        public void TriggerTrainerDefeatedEvent(string trainerName)
        {
            if (TrainerDefeatedEvent != null)
            {
                TrainerDefeatedEvent(this, new TrainerDefeatedEventArgs(), trainerName);
            }
        }

        // QUAND UN DRESSEUR POKEMON EST VAINCU TU RECUPERE SON NOM DONC FAUT GERER CE QUIL SE PASSE
        // EX: JAI BATTU PROFESSEUR MEGABICEPS, JE VERIFIE SI DANS MES QUETES EN COURS JE DEVAIS LE BATTRE
        // SI OUI, ALORS TU FINI LA QUETE AVEC CompleteQuest()
        private void OnTrainerDefeated(object sender, TrainerDefeatedEventArgs e, string trainerName)
        {
            Console.WriteLine(trainerName + " Trainer has been defeated!");
            System.Threading.Thread.Sleep(10000);
        }

        // CA C POUR RECUP LEVENT TAS CAPTÉ
        public void SubscribeToTrainerDefeatedEvent()
        {
            TrainerDefeatedEvent += OnTrainerDefeated;
        }

        // COMPLETER LA QUETE
        public void CompleteQuest(Quest quest)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.IsCompleted = true;
        }

    }

    // CLASSE QUETE AVEC SES INFOS
    public class Quest
    {
        public string Name { get; }
        public string Description { get; }
        public bool IsCompleted { get; set; }

        public Quest(string name, string description)
        {
            Name = name;
            Description = description;
            IsCompleted = false;
        }

    }

    // CA JE SAIS PLUS SI CA SERT JCROIS PAS
    public class DefeatTrainerQuest : Quest
    {
        public DefeatTrainerQuest(string name, string description) : base(name, description) { }
    }

    // CA NON PLUS
    public class CapturePokemonQuest : Quest
    {
        public string PokemonToCapture { get; }

        public CapturePokemonQuest(string name, string description, string pokemonToCapture) : base(name, description)
        {
            PokemonToCapture = pokemonToCapture;
        }
    }

    // ET CA PAS TOUCHE STP MERCI
    public class QuestEventHandler
    {
        public event TrainerDefeatedEventHandler TrainerDefeatedEvent;
        public event PokemonCapturedEventHandler PokemonCapturedEvent;
        public event PokemonCollectedEventHandler PokemonCollectedEvent;

    }

}