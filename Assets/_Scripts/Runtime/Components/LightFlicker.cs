using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProtoPlat.Components
{
    [RequireComponent(typeof(Light2D))]
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField] private float maxIntensity = 1.0f;
        [SerializeField] private float minIntensity = 0.9f;
        [SerializeField] private float flickerSpeed = 0.05f;

        private Light2D _light;
        private float _intensity;
        private float _timeCounter;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _intensity = _light.intensity;
        }

        private void Update()
        {
            _timeCounter += Time.deltaTime;

            if (_timeCounter > flickerSpeed)
            {
                _timeCounter = 0;
                _intensity = Random.Range(minIntensity, maxIntensity);
            }
            else
            {
                _light.intensity = Mathf.Lerp(_light.intensity, _intensity, _timeCounter / flickerSpeed);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (maxIntensity < minIntensity)
                maxIntensity = minIntensity;
        }
#endif
    }
}
