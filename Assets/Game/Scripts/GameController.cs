using System.Collections;
using System.Linq;
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
    [SerializeField]
    private Character[] _playerCharacters;

    [SerializeField]
    private Character[] _enemyCharacters;

    private void Start()
    {
        StartCoroutine(LevelLoop());
    }

    private IEnumerator LevelLoop()
    {
        while (true)
        {
            foreach (var player in _playerCharacters)
            {
                if (AreCharactersAlive(_playerCharacters) && AreCharactersAlive(_enemyCharacters))
                {
                    var enemyTarget = GetTarget(_enemyCharacters);
                    yield return player.Attack(enemyTarget);
                }

                yield return new WaitForSeconds(1f);
            }

            foreach (var enemy in _enemyCharacters)
            {
                if (AreCharactersAlive(_playerCharacters) && AreCharactersAlive(_enemyCharacters))
                {
                    var playerTarget = GetTarget(_playerCharacters);
                    yield return enemy.Attack(playerTarget);
                }

                yield return new WaitForSeconds(1f);
            }

            if (AreAllCharactersDead(_playerCharacters) || AreAllCharactersDead(_enemyCharacters))
            {
                yield break;
            }
        }
    }

    private bool AreCharactersAlive(Character[] characters, int minAlive = 1)
    {
        int countAlive = 0;
        foreach (var character in characters)
        {
            if (character.IsAlive)
            {
                countAlive += 1;
                if (countAlive >= minAlive)
                    return true;
            }
        }

        return false;

        // LINQ
        // var numAlive = characters.Count(character => character.IsAlive);
        // return numAlive >= minAlive;
    }

    private bool AreAllCharactersDead(Character[] characters)
    {
        foreach (var character in characters)
        {
            if (character.IsAlive)
            {
                return false;
            }
        }

        return true;
    }

    private Character GetTarget(Character[] characters)
    {
        return characters.First(character => character.IsAlive);
    }
}