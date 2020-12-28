using LB.GameMechanics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace LB.Quests
{															
    public class QuestLauncher :  PickupAble
    {
		public Quest quest;


        public override void Execute()
        {
            quest.Init();
        }
        public override string GetName() => $"Start {quest.Name} quest";

    }
	
}
