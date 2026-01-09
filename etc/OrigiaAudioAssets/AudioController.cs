using UnityEngine;

public class AudioController : MonoBehaviour {
    [SerializeField]
    AudioClip[] bgm;

    [SerializeField]
    AudioClip[] se;

    [SerializeField]
    AudioClip[] cv;

    AudioSource[] audioSources;

    enum AUDIO {
        bgm,
        se,
        cv
    }

    public static class Constants {
        public const int bgm_title = 0;
        public const int bgm_spring = 1;
        public const int bgm_summer = 2;
        public const int bgm_autumn = 3;
        public const int bgm_battle = 4;
        public const int bgm_finalbattle = 5;
        public const int bgm_gameresult = 6;
        public const int bgm_winning = 7;
        public const int bgm_ending = 8;
    }

    private void Awake() {
        audioSources = GetComponents<AudioSource>();
    }

    public void ChangeBgm(int num) {
        audioSources[(int)AUDIO.bgm].clip = bgm[num];
        audioSources[0].Play();
    }

    public void SePlay(int num) {
        audioSources[(int)AUDIO.se].PlayOneShot(se[num]);
    }

    public void CvPlay(int num) {
        audioSources[(int)AUDIO.cv].PlayOneShot(cv[num]);
    }