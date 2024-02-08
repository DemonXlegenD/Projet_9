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

        public void CompleteQuest(Quest quest)
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.IsCompleted = true;
        }

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

    public class QuestEventHandler
    {
        public event TrainerDefeatedEventHandler TrainerDefeatedEvent;
        public event PokemonCapturedEventHandler PokemonCapturedEvent;
        public event PokemonCollectedEventHandler PokemonCollectedEvent;

    }

}