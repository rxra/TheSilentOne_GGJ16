using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	public bool destroySingletonIfNewInstance = false;
	public bool dontDestroySingletonOnLoad = false;

	public static T instance {
		get {
			return s_Instance;
		}
	}

	protected bool destroying
	{
		get
		{
			return _destroying;
		}
	}

	private static T s_Instance = null;
	private bool _destroying = false;

	public virtual bool Awake() {
		_destroying = false;
		if (s_Instance!=null) {
			if (destroySingletonIfNewInstance) {
				if (!dontDestroySingletonOnLoad) {
					Debug.LogWarning("Singleton instance (" + typeof(T) + ") already exist. Destroy this one!");
				}
				_destroying = true;
				GameObject.Destroy(gameObject);
			} else {
				Debug.LogError("Singleton instance (" + typeof(T) + ") already exist.");
			}
			return false;
		}

		s_Instance = gameObject.GetComponent<T>();

		if (dontDestroySingletonOnLoad) {
			DontDestroyOnLoad(gameObject);
		}

		return true;
	}

}
