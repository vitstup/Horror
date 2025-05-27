using TMPro;
using UnityEngine;

public class QuestsHudHandler : MonoBehaviour
{
    [SerializeField] private QuestHandler questHandler;

    [SerializeField] private TextMeshProUGUI questText;

    private void Update()
    {
        questText.text = questHandler.currentQuest != null ? questHandler.currentQuest.questDescription : "";
    }
}