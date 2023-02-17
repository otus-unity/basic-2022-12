using Game.Scripts.Characters;

namespace Game.Scripts.Helpers
{
    public static class CharacterHelper
    {
        public const float MeleeAttackDistanceThreshold = 0.1f;

        public static int GetIndexOf(Character character, Character[] characters)
        {
            for (var i = 0; i < characters.Length; i++)
            {
                if (characters[i] == character)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}