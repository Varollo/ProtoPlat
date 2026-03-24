using UnityEngine;

namespace ProtoPlat.Animation
{
    [CreateAssetMenu(fileName = "New Sprite Animation", menuName = "Ribbons/Sprite Animation")]
    public class SpriteAnimationSO : ScriptableObject
    {
        [SerializeField] private bool _loop = true;
        [SerializeField] private Sprite[] _frames;

        public Sprite[] Frames => _frames;
        public bool Loop => _loop;
    }
}
