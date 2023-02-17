using System;
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


    private Weapon _sniperRifle = new Weapon(WeaponType.SniperRifle, 5);

    private Queue _turns = new Queue();

    private Character _selectedTarget;
    private bool _isTargetSelectionConfirmed;


    private void Start()
    {
        foreach (var character in _playerCharacters)
        {
            _turns.Enqueue(character);
        }

        foreach (var character in _enemyCharacters)
        {
            _turns.Enqueue(character);
        }

        StartCoroutine(LevelLoop());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectRandomTarget(_enemyCharacters);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _isTargetSelectionConfirmed = true;
        }
    }

    private IEnumerator LevelLoop()
    {
        foreach (var turn in GetTurns())
        {
            if (turn is Character character)
            {
                if (character.IsAlive)
                {
                    _isTargetSelectionConfirmed = false;

                    if (IsPlayerCharacter(character))
                    {
                        Debug.Log("GameController.LevelLoop: SELECT TARGET");

                        SelectRandomTarget(_enemyCharacters);
                    }
                    else
                    {
                        AutoSelectTarget(_playerCharacters);
                    }

                    yield return new WaitUntil(() => _isTargetSelectionConfirmed);

                    yield return character.Attack(_selectedTarget);

                    yield return new WaitForSeconds(1f);

                    // Pushing this character back to queue
                    _turns.Enqueue(character);
                }
            }
            else if (turn is Weapon weapon)
            {
                var enemy = _enemyCharacters.FirstOrDefault(character => character.IsAlive);
                if (enemy != null && enemy.IsAlive)
                {
                    enemy.TakeDamage(weapon.Damage);
                }

                yield return new WaitForSeconds(1f);

                // Do NOT push sniper rifle back to queue
            }

            if (AreAllCharactersDead(_enemyCharacters))
            {
                GameWon();
                yield break;
            }

            if (AreAllCharactersDead(_playerCharacters))
            {
                GameLost();
                yield break;
            }
        }
    }

    private IEnumerable GetTurns()
    {
        while (true)
        {
            var dequeue = _turns.Dequeue();
            yield return dequeue;
        }
    }

    private bool IsPlayerCharacter(Character character)
    {
        return _playerCharacters.Contains(character);
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

    private void SelectRandomTarget(Character[] characters)
    {
        var aliveCharacters = characters.Where(character => character.IsAlive).ToArray();
        var random = new System.Random();
        var randomCharactersIndex = random.Next(aliveCharacters.Length);

        Debug.Log($"GameController.SelectTarget: randomEnemyIndex = {randomCharactersIndex}");

        _selectedTarget = aliveCharacters[randomCharactersIndex];
    }

    private void AutoSelectTarget(Character[] characters)
    {
        _selectedTarget = characters.First(character => character.IsAlive);

        _isTargetSelectionConfirmed = true;
    }

    public void CallSniper()
    {
        if (_turns.Contains(_sniperRifle))
            return;

        _turns.Enqueue(_sniperRifle);
    }

    private void GameWon()
    {
        Debug.Log("GameController.GameWon: ");
    }

    private void GameLost()
    {
        Debug.Log("GameController.GameLost: ");
    }
}