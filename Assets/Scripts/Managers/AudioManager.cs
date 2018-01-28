using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager singleton;
	public AudioClip FightMusic;
	public AudioClip MenuMusic;
	public AudioClip[] countdown = new AudioClip[4];
	public List<AudioClip> sfxList = new List<AudioClip>();
	public Dictionary<string, AudioClip> sfxDico = new Dictionary<string, AudioClip>();
	public AudioSource sfxSource;
	public AudioSource musicSource;
	public AudioMixer mixer;
	private AudioClip _ac;

	void Awake()
	{
		singleton = this;

		foreach(AudioClip clip in sfxList){
			sfxDico.Add(clip.name, clip);
		}
	}

	public void PlayMusic(AudioClip ac){
		musicSource.DOFade(1f, 1f);
		if(musicSource.isPlaying){
			float volume = musicSource.volume;
			musicSource.DOFade(0f, 0.5f).OnComplete(
				()=>{musicSource.volume = volume; musicSource.PlayOneShot(ac);}
			);
		}else{
			musicSource.PlayOneShot(ac);
		}
	}

	public void StopMusic(){
		musicSource.DOFade(0f, 1f).OnComplete(()=> musicSource.Stop());
	}

	public void PlayLoop(AudioClip ac, AudioSource src){
		src.pitch = 1f;
		src.loop = true;
		src.clip = ac;
		src.Play();
	}
	public void PlaySFX(AudioClip ac){
		sfxSource.pitch = 1f;
		sfxSource.PlayOneShot(ac);
	}

	public void PlayAt(AudioClip ac, AudioSource src){
		src.Stop();
		src.loop = false;
		src.PlayOneShot(ac);
	}

	public void PlayRandomize(AudioClip ac, AudioSource src = null){
		if(src == null) src = sfxSource;

		src.pitch = Random.Range(0.7f, 1.3f);
		src.PlayOneShot(ac);
	}

	public AudioClip GetSFXclip(string name){
		_ac = null;
		if(sfxDico.TryGetValue(name, out _ac)){
			return _ac;
		}

		return null;
	}
}
