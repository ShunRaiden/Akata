using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	public GameObject loadingCanvas;
	public GameObject[] loaderUI;
	public Image loadSlider;

	public IEnumerator LoadingScreenTimer(string index)
	{
		loadSlider.fillAmount = 0;
		loadingCanvas.SetActive(true);
        loaderUI[Random.Range(0,1)].SetActive(true);

		Time.timeScale = 1;

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
		asyncOperation.allowSceneActivation = false;
		float progress = 0;
		while (!asyncOperation.isDone)
		{
			progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
			loadSlider.fillAmount = progress;
			if (progress >= 0.9f)
			{
				loadSlider.fillAmount = 1;
				asyncOperation.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
