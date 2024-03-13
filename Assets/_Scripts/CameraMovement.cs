using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] GamePlayManager _gamePlaymanager;
    [SerializeField] Camera _camera;
    [SerializeField] BoundArea _playArea;

    [Header("Values")]
    [SerializeField][Range(0, 1)] float _thresholdPercent;
    [SerializeField][Range(0, 0.2f)] float _maxMovePercent;

    [Header("Gizmos")]
    [SerializeField] Color _gizmosViewColor = Color.yellow;
    [SerializeField] Color _gizmosThresholdColor = Color.magenta;
    [SerializeField] Color _gizmosLimitColor = Color.red;

    Vector3 _targetPosition = Vector3.zero;
    Bounds _viewBounds;
    Bounds _thresholdBounds;
    Bounds _limitBounds;

    private void Awake()
    {
        float widthView = _camera.orthographicSize * _camera.aspect;
        float heightView = _camera.orthographicSize;
        _viewBounds = new Bounds(
            Vector3.zero,
            new Vector3(widthView * 2, heightView * 2)
        );

        float widthThreshold = _viewBounds.extents.x * _thresholdPercent;
        float heightThreshold = _viewBounds.extents.y * _thresholdPercent;
        _thresholdBounds = new Bounds(
            Vector3.zero,
            new Vector3(widthThreshold * 2, heightThreshold * 2)
        );

        float widthLimit = math.max(_playArea.Bound.extents.x - widthView, 0);
        float heightLimit = math.max(_playArea.Bound.extents.y - heightView, 0);
        _limitBounds = new Bounds(
            _playArea.Bound.center,
            new Vector3(widthLimit * 2, heightLimit * 2)
        );
    }

    private void Start()
    {
        _playerInput.MousePosition += UpdateTargetPosition;
        _camera.transform.position = new Vector3(
            _playArea.Bound.center.x,
            _playArea.Bound.center.y,
            -10
        );
    }

    private void FixedUpdate()
    {
        if (_gamePlaymanager.CanMoveCamera() && !IsInsideThreshold(_targetPosition))
        {
            Vector2 movePercent = Vector2.zero;
            Bounds threshold = new(
                _camera.transform.position,
                _thresholdBounds.size
            );

            if (_targetPosition.x < threshold.min.x)
            {
                movePercent.x = math.abs(_targetPosition.x - threshold.min.x) / (_viewBounds.extents.x - _thresholdBounds.extents.x);
            }
            else if (_targetPosition.x > threshold.max.x)
            {
                movePercent.x = math.abs(_targetPosition.x - threshold.max.x) / (_viewBounds.extents.x - _thresholdBounds.extents.x);
            }

            if (_targetPosition.y < threshold.min.y)
            {
                movePercent.y = math.abs(_targetPosition.y - threshold.min.y) / (_viewBounds.extents.y - _thresholdBounds.extents.y);
            }
            else if (_targetPosition.y > threshold.max.y)
            {
                movePercent.y = math.abs(_targetPosition.y - threshold.max.y) / (_viewBounds.extents.y - _thresholdBounds.extents.y);
            }

            float percent = math.min(1, math.max(0, math.max(movePercent.x, movePercent.y)));
            Vector3 position = Vector3.Lerp(_camera.transform.position, _targetPosition, percent * _maxMovePercent);

            float x = math.min(_limitBounds.max.x, math.max(_limitBounds.min.x, position.x));
            float y = math.min(_limitBounds.max.y, math.max(_limitBounds.min.y, position.y));
            _camera.transform.position = new(x, y, -10);
        }
    }

    private bool IsInsideThreshold(Vector3 point)
    {
        Bounds threshold = new(
            _camera.transform.position,
            _thresholdBounds.size
        );
        return threshold.Contains(point);
    }

    void UpdateTargetPosition(Vector3 position)
    {
        _targetPosition = new(
            position.x,
            position.y,
            -10
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosViewColor;
        Gizmos.DrawWireCube(_viewBounds.center + _camera.transform.position, _viewBounds.size);
        Gizmos.color = _gizmosThresholdColor;
        Gizmos.DrawWireCube(_thresholdBounds.center + _camera.transform.position, _thresholdBounds.size);
        Gizmos.color = _gizmosLimitColor;
        Gizmos.DrawWireCube(_limitBounds.center, _limitBounds.size);
    }
}
