using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEffects
{
    public class PlayRandomClip : MonoBehaviour
    {
        public bool playOnAwake = true;

        [SerializeField] private AudioSource m_audioSource = null;

        [SerializeField] private AudioClip[] clips = null;

        private void OnEnable()
        {
            if (playOnAwake) PlayClip();
        }

        public void PlayClip()
        {
            var clip = clips[Random.Range(0, clips.Length)];

            if (m_audioSource == null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }
            else
            {
                m_audioSource.PlayOneShot(clip);
            }
        }
    }
}