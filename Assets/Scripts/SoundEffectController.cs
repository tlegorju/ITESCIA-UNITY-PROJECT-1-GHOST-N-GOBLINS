using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] AudioClip[] spawnClips;
    [SerializeField] AudioClip[] runClips;
    [SerializeField] AudioClip[] attackClips;
    [SerializeField] AudioClip[] hurtedClip;
    [SerializeField] AudioClip[] diesClips;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        EntityController controller = GetComponent<EntityController>();
        controller.OnSpawn += PlaySpawn;
        controller.OnMove += PlayRun;
        controller.OnAttack += PlayAttack;
        controller.OnHurted += PlayHurted;
        controller.OnDies += PlayDies;
    }

    private void PlaySpawn()
    {
        if (spawnClips.Length <= 0)
            return;
        source.clip = spawnClips[Random.Range(0, spawnClips.Length)];
        source.Play();
    }

    private void PlayRun()
    {
        if (runClips.Length <= 0 || source.isPlaying)
            return;
        source.clip = runClips[Random.Range(0, runClips.Length)];
        source.Play();
    }

    private void PlayAttack()
    {
        if (attackClips.Length <= 0)
            return;
        source.clip = attackClips[Random.Range(0, attackClips.Length)];
        source.Play();
    }

    private void PlayHurted()
    {
        if (hurtedClip.Length <= 0)
            return;
        source.clip = hurtedClip[Random.Range(0, hurtedClip.Length)];
        source.Play();
    }

    private void PlayDies()
    {
        if (diesClips.Length <= 0)
            return;
        source.clip = diesClips[Random.Range(0, diesClips.Length)];
        source.Play();
    }
}
