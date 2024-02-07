using System;

namespace NQuest
{
    public delegate void QuestEventHandler(object sender, QuestEventArgs e);

    public class QuestEventArgs : EventArgs
    {
        public string Message { get; }

        public QuestEventArgs(string message)
        {
            Message = message;
        }
    }

    public enum QuestType
    {
        Defeat,
        Capture,
        Collect
    }

    public class Quest
    {
        private string _name;
        private QuestType _type;
        private string _description;

        public event QuestEventHandler QuestEventOccurred;

        public Quest(string name, QuestType type, string description)
        {
            _name = name;
            _type = type;
            _description = description;
        }

        protected virtual void OnQuestEventOccurred(QuestEventArgs e)
        {
            QuestEventOccurred?.Invoke(this, e);
        }

        public void HandleEvent(string message)
        {
            Console.WriteLine(message);
        }

        public void StartQuest()
        {
            OnQuestEventOccurred(new QuestEventArgs("La quête a démarré."));
        }
    }

}