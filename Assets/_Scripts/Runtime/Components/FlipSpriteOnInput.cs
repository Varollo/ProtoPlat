using ProtoPlat.Input;
using UnityEngine;

namespace ProtoPlat.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlipSpriteOnInput : MonoBehaviour
    {
        private SpriteRenderer _sr;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (InputManager.Move.Value.x < 0)
                _sr.flipX = true;
            else if (InputManager.Move.Value.x > 0)
                _sr.flipX = false;
        }
    }
}
