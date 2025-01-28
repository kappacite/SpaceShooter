using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundEffect : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Récupère le composant AudioSource attaché à cet objet
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Vérifie si le clic gauche de la souris est effectué
        if (Input.GetMouseButtonDown(0)) // "0" correspond au clic gauche
        {
            audioSource.Play();
        }
    }
}
    
