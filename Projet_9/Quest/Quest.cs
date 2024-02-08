using System;
using System.Collections.Generic;

namespace NQuest
{
    public delegate void TrainerDefeatedEventHandler(object sender, TrainerDefeatedEventArgs e, string trainerName);
    public delegate void PokemonCapturedEventHandler(object sender, PokemonCapturedEventArgs e);
    public delegate void PokemonCollectedEventHandler(object sender, PokemonCollectedEventArgs e);


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

    public class QuestManager
    {
        public event TrainerDefeatedEventHandler TrainerDefeatedEvent;
        public event PokemonCapturedEventHandler PokemonCapturedEvent;
        public event PokemonCollectedEventHandler PokemonCollectedEvent;

        List<Quest> availableQuests;
        List<Quest> activeQuests;
        List<Quest> completedQuests;

        public QuestManager()
        {
            availableQuests = new List<Quest>();
            activeQuests = new List<Quest>();
            completedQuests = new List<Quest>();
        }

       public void AddAvailableQuest(Quest quest)
        {
            availableQuests.Add(quest);
        }

        public void StartQuest(Quest quest)
        {
            availableQuests.Remove(quest);
            activeQuests.Add(quest);
        }

<<<<<<< HEAD
        public void TriggerTrainerDefeatedEvent(string trainerName)
        {
            if (TrainerDefeatedEvent != null)
            {
                TrainerDefeatedEvent(this, new TrainerDefeatedEventArgs(), trainerName);
            }
        }

        private void OnTrainerDefeated(object sender, TrainerDefeatedEventArgs e, string trainerName)
        {
            Console.WriteLine(trainerName + " Trainer has been defeated!");
            System.Threading.Thread.Sleep(10000);
        }

        public void SubscribeToTrainerDefeatedEvent()
        {
            TrainerDefeatedEvent += OnTrainerDefeated;
        }
=======
        public void CompleteQuest(Quest quest)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.IsCompleted = true;
        }

>>>>>>> 0c87498cf7b1f07d8d04220266f2f715715daa1c
    }

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

    public class DefeatTrainerQuest : Quest
    {
        public DefeatTrainerQuest(string name, string description) : base(name, description) { }
    }

    public class CapturePokemonQuest : Quest
    {
        public string PokemonToCapture { get; }

        public CapturePokemonQuest(string name, string description, string pokemonToCapture) : base(name, description)
        {
            PokemonToCapture = pokemonToCapture;
        }
    }

<<<<<<< HEAD
=======
    public class QuestEventHandler
    {
        public event TrainerDefeatedEventHandler TrainerDefeatedEvent;
        public event PokemonCapturedEventHandler PokemonCapturedEvent;
        public event PokemonCollectedEventHandler PokemonCollectedEvent;

    }

>>>>>>> 0c87498cf7b1f07d8d04220266f2f715715daa1c
}