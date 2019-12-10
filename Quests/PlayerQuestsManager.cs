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
           
        }

        private void Update()
        {
            if (availableQuests.Count != 0)
                currentQuest = availableQuests[0];


            if (currentQuest != null)
                currentQuest.currentQuestGoal = currentQuest.QuestGoals[0];


            if (availableQuests.Count != 0)
            {
                currentQuest = availableQuests[0];
            }

            if(currentQuest != null)
            {
                questNameText.text = currentQuest.Name;

                if(currentQuest.QuestGoals.Count != 0)
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
