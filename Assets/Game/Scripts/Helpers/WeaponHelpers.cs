using Game.Scripts.Characters;

namespace Game.Scripts.Helpers
{
    public static class WeaponHelpers
    {
        public static string GetAnimationNameFor(WeaponType weaponType)
        {
            string result;
            switch (weaponType)
            {
                case WeaponType.Gun:
                    result = "IsShooting";
                    break;
                case WeaponType.BaseballBat:
                    result = "IsHitting";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        }
    }
}