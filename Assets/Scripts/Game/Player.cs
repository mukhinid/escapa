using System;
using Escapa.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using static Escapa.Core.InputSystem_Actions;

namespace Escapa.Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Scale))]
    public sealed class Player : MonoBehaviour, IPlayerActions
    {
        public event Action Died;
        public event Action Moved;
        public event Action Pressed;
        public event Action Stopped;

        [SerializeField]
        private MainCamera _camera;

        private InputSystem_Actions _actions;

        private bool isTouched;
        private Vector2 oldPosition;
        private Vector2 targetPosition;

        private const float Width = 0.75f;

        private void OnEnable()
        {
            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.Player.SetCallbacks(this);
            }
            _actions.Player.Enable();
        }

        private void Update()
        {
            if (isTouched)
                transform.position = targetPosition;

            if (Vector2.Distance(targetPosition, oldPosition) > float.Epsilon)
                Moved?.Invoke();
            else
                Stopped?.Invoke();
            oldPosition = targetPosition;
        }

        private void OnCollisionEnter2D() => Died?.Invoke();

        private void OnDisable()
        {
            _actions.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!_camera)
            {
                return;
            }

            var position = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            isTouched = IsTouched(position, targetPosition);

            if (isTouched)
            {
                targetPosition = position;
                Pressed?.Invoke();
            }
        }

        private static bool IsTouched(Vector2 position, Vector2 touchPosition) => position.x - Width < touchPosition.x
                                                                               && touchPosition.x < position.x + Width
                                                                               && position.y - Width < touchPosition.y
                                                                               && touchPosition.y < position.y + Width;
    }
}
