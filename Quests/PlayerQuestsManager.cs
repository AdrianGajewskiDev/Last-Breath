using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LB.Quests
{
    public class PlayerQuestsManager : MonoBehaviour
    {
        public static PlayerQuestsManager Singleton;

        public Quest currentQuest;
        public List<Quest> availableQuests;

        public Text questNameText;
        public Text questGoalDescriptionText;

        private void Awake()
        {
            Singleton = this;
            if(currentQuest != null)
                currentQuest.currentQuestGoal = currentQuest.QuestGoals[0];
        }

        private void Update()
        {
            if(currentQuest != null)
            {
                questNameText.text = currentQuest.Name;
                questGoalDescriptionText.text = currentQuest.currentQuestGoal.Description;
            }
            else
            {
                questNameText.text = string.Empty;
                questGoalDescriptionText.text = string.Empty;

            }

        }
    }

}
