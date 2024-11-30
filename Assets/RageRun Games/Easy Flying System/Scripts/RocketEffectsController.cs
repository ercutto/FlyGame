using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class RocketEffectsController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem smokeParticleSystem;
        [SerializeField] private ParticleSystem flameParticleSystem;

        private void Start()
        {
            smokeParticleSystem.Stop();
            flameParticleSystem.Stop();
        }

        public void StopAllEffects()
        {
            smokeParticleSystem.Stop();
            flameParticleSystem.Stop();
        }

        public void StartAllEffects()
        {
            smokeParticleSystem.Play();
            flameParticleSystem.Play();
        }

    }
}
