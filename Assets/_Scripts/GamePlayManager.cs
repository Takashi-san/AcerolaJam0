using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GamePlayManager : MonoBehaviour
{
    GameState _state = GameState.None;
    GameState _pausedState = GameState.None;

    [SerializeField] BoundSpawner _spawnArea;
    [SerializeField] SpriteRenderer _character;

    [SerializeField] int _alienCount = 0;
    [SerializeField] Color _alienColor = Color.red;

    [SerializeField] int _commonerCount = 0;
    [SerializeField] Color _commonerColor = Color.blue;

    enum GameState
    {
        None,
        Setup,
        Intro,
        Playing,
        Pause,
        Result,
    }

    public void TogglePause()
    {
        switch (_state)
        {
            case GameState.Pause:
                _state = _pausedState;
                _pausedState = GameState.None;
                break;

            case GameState.Setup:
                break;

            default:
                _pausedState = _state;
                _state = GameState.Pause;
                break;
        }
    }

    private void Start()
    {
        Setup();
        _state = GameState.Playing;
    }

    private void Setup()
    {
        _state = GameState.Setup;
        // TODO: fazer tudo de forma assíncrona.

        var aliens = _spawnArea.Spawn(_character, _alienCount);
        foreach (var alien in aliens)
        {
            alien.color = _alienColor;
        }

        var commoners = _spawnArea.Spawn(_character, _commonerCount);
        foreach (var commoner in commoners)
        {
            commoner.color = _commonerColor;
        }
    }
}
