using LB.GameMechanics;
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
    }
	
}
