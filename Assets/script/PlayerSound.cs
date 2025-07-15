using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip jumpClip; // El clip de audio para el salto (arr�stralo desde el Inspector)
    public AudioSource audioSource; // El componente AudioSource en el jugador

    private void Start()
    {
        // Obtener el componente AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay un AudioSource, a�adir uno
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Reproducir sonido al saltar
        // Usamos GetButtonDown para que el sonido se reproduzca una sola vez por pulsaci�n
        if (Input.GetButtonDown("Jump"))
        {
            // Reproducir el SFX (Sound Effect) con un volumen de 0.3f
            // Aseg�rate de que 'jumpClip' est� asignado en el Inspector
            if (jumpClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpClip, 0.3f);
            }
            else
            {
                Debug.LogWarning("Clip de salto o AudioSource no asignado en PlayerSound script.");
            }
        }
    }

    // Puedes a�adir m�s funciones para otros sonidos, por ejemplo:
    public void PlayDamageSound(AudioClip damageClip)
    {
        if (damageClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageClip, 0.5f); // Volumen ajustable
        }
    }
}
