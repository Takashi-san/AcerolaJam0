using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GamePlayManager : MonoBehaviour
{
    GameState _state = GameState.None;
    GameState _pausedState = GameState.None;

    [SerializeField] PlayerInput _input;

    [SerializeField] BoundSpawner _spawnArea;
    [SerializeField] Minion _character;
    [SerializeField] StageData _stageData;
    [SerializeField] Color _alienColor = Color.red;
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

    public bool KillMinion(Minion minion)
    {
        return _state == GameState.Playing;
    }

    private void Start()
    {
        _input.OnPressEsc += TogglePause;
        Setup();
        _state = GameState.Playing;
    }

    private void Setup()
    {
        _state = GameState.Setup;
        // TODO: fazer tudo de forma assíncrona.

        var aliens = _spawnArea.Spawn(_character, _stageData.EnemyCount);
        foreach (var alien in aliens)
        {
            alien.GamePlayManager = this;
            alien.Visual.color = _alienColor;
            alien.Type = MinionType.Enemy;
        }

        var commoners = _spawnArea.Spawn(_character, _stageData.CommonerCount);
        foreach (var commoner in commoners)
        {
            commoner.GamePlayManager = this;
            commoner.Visual.color = _commonerColor;
            commoner.Type = MinionType.Commoner;
        }
    }
}
