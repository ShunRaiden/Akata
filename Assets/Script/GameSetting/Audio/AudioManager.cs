using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance { get { return _instance; } }
    private static AudioManager _instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy the new instance, not this script
            return;
        }

        _instance = this;
    }
    #endregion

    [Header("------- Audio Source -------")]
	public AudioSource m_AudioSource;
	public AudioSource s_AudioSource;
    public AudioSource d_AudioSource;
	public AudioSource v_AudioSource;

	[Header("------- Audio Clip -------")]
	[Header("Music")]
	public AudioClip[] m_bg_Center;
    public AudioClip m_bg_MainMenu;
    public AudioClip m_bg_State;


	[Header("Sound")]
	public AudioClip s_SelectBT;
    public AudioClip s_HoverBT;
    public AudioClip s_Dialouge;
    public AudioClip s_Jump;
    public AudioClip s_Dash;
    public AudioClip[] s_Slash;
    public AudioClip s_EnemyHit;
    public AudioClip s_Block;
    public AudioClip s_Parry;

    [Header("Voice")]
	public AudioClip v_Player;
    public AudioClip v_Narrator;
    public AudioClip v_NPC_Older;
	public AudioClip v_NPC_Chef;
    public AudioClip v_NPC_Kid;
    public AudioClip v_NPC_Miko;

    private void Start()
	{
        if(SceneManager.GetActiveScene().name == "Center")
        {
            PlayRandomTrack();
        }

    }

    private void Update()
    {
        if (!m_AudioSource.isPlaying && SceneManager.GetActiveScene().name == "Center")
        {
            PlayRandomTrack();
        }
    }

    public void PlaySFX(AudioClip clip)
	{
		s_AudioSource.PlayOneShot(clip);
	}

	public void PlayVoice(AudioClip clip)
    {
        v_AudioSource.PlayOneShot(clip);
    }

	public void StopSFX()
	{
        s_AudioSource.Stop();
	}

    void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, m_bg_Center.Length);
        m_AudioSource.clip = m_bg_Center[randomIndex];
        m_AudioSource.Play();
    }

}
