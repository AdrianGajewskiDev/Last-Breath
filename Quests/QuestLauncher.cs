using LB.GameMechanics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LB.Quests
{															
    public class QuestLauncher : MonoBehaviour, IPickupAble
    {
		public Quest quest;
		
		public void Execute()
        {
            quest.Init();
        }
        public string GetName() => $"Start a {quest.Name} quest";

    }
	
}
