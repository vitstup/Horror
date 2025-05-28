using System;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    public Quest currentQuest { get; private set; }

    public void GiveQuest(Quest quest)
    {
        if (currentQuest != null) return;

        currentQuest = quest;

        currentQuest.OnQuestCompleted += OnQuestCompleted;
    }

    private void Update()
    {
        currentQuest?.Tick();
    }

    private void OnQuestCompleted(Quest quest)
    {
        currentQuest.OnQuestCompleted -= OnQuestCompleted;

        currentQuest = null;
    }

    private void OnDestroy()
    {
        if (currentQuest != null)
            currentQuest.OnQuestCompleted -= OnQuestCompleted;
    }

    public void OnQuestCompleted(Quest quest, Action<Quest> callback)
    {
        void Handler(Quest q)
        {
            callback(q);
            quest.OnQuestCompleted -= Handler;
        }
        quest.OnQuestCompleted += Handler;
    }
}