using UnityEngine;

namespace Muramasa.NonPlayerCharacter
{
    [CreateAssetMenu(fileName = "EntitySettings", menuName = "Settings/EntitySettings", order = 0)]
    public class EntitySettings : ScriptableObject
    {
        public int entityId = 0;
        public string entityName = "Bob";
        
        public int entityLevel = 1;
        public float movementSpeed = 5f;
    }
}