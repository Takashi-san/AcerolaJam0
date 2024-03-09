using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GamePlayManager GamePlayManager;
    public SpriteRenderer Visual;
    public MinionType Type;

    [SerializeField] HitBox _hitBox;

    private void Awake()
    {
        _hitBox.OnPointerDownAction = OnPointerDown;
    }

    void OnPointerDown()
    {
        Debug.Log($"Selected minion type: {Type}");
        if (GamePlayManager.KillMinion(this))
        {
            Destroy(gameObject);
        }
    }
}
