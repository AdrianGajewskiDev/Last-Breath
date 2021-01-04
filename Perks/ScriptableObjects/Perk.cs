using UnityEngine;

namespace LB.Perks 
{
    [CreateAssetMenu(menuName = "Assets/Create Perk")]
    public class Perk : ScriptableObject
    {
        public PerkKey PerkKey;
        public GameObject OriginalPrefab;
        public GameObject HologramPrefab;
    }

}
