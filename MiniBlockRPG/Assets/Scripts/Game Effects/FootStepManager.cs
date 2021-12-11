using UnityEngine;


namespace GameEffects
{
    public class FootStepManager : MonoBehaviour
    {
        // FIXME: Implement surface detection

        [SerializeField] private AudioSource m_AudioSource = null;
        [SerializeField] private AudioClip[] footSteps = null;

        public void PlayFootStep()
        {
            var clip = footSteps[Random.Range(0, footSteps.Length)];

            if (m_AudioSource == null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
            else
            {
                m_AudioSource.PlayOneShot(clip);
            }
        }
    }
}