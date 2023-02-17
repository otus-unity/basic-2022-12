using System.Collections;
using Game.Scripts.Characters;
using UnityEngine;

// Show menu
// Player selects "Start Game"
// Show game intro
/* Game loop start */
// Load level 1
// Show level 1 intro

/* Level loop start */
// Character 1 Turn
// Character 2 Turn
// ...
// Character N Turn
/* Level loop finish */

// Load level 2
// ...

/* Game loop finish */

// Show game over (success or fail)
public class GameController : MonoBehaviour
{
    public Character Player;
    public Character Enemy;

    private void Start()
    {
        StartCoroutine(LevelLoop());
    }

    private IEnumerator LevelLoop()
    {
        while (true)
        {
            if (Player.Health > 0 && Enemy.Health > 0)
            {
                yield return Player.Attack(Enemy);
            }

            yield return new WaitForSeconds(1f);

            if (Player.Health > 0 && Enemy.Health > 0)
            {
                yield return Enemy.Attack(Player);
            }

            yield return new WaitForSeconds(1f);

            if (Player.Health <= 0 || Enemy.Health <= 0)
            {
                yield break;
            }
        }
    }
}