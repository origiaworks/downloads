using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// GameManagerなどにアタッチして使用する。
/// 付属のパネルを_loadUIPrefabに入れる。
/// 外部からNextSceneRunを呼び出す。
/// 引数にはBuildIndexを指定する。
/// </summary>
public class NextScene : MonoBehaviour {
    [SerializeField]
    [Tooltip("シーンロード中に表示するUI画面")]
    private GameObject _loadUIPrefab;

    private AsyncOperation _async;
    private Slider _slider;

    public void NextSceneRun(int sceneNum) {
        var _prefab = Instantiate(_loadUIPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);        
        _slider = _prefab.GetComponent<LoadingPanel>().slider;
        StartCoroutine(LoadData(sceneNum));
    }

    IEnumerator LoadData(int sceneNum) {
        _async = SceneManager.LoadSceneAsync(sceneNum);
        while (!_async.isDone) {
            var progressVal = Mathf.Clamp01(_async.progress / 0.9f);
            _slider.value = progressVal;
            yield return null;
        }
    }
}
