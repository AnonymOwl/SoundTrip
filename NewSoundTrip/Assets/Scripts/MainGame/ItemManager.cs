using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

	public ParticleSystem	deathParticles;
	private ParticleSystem	partInstance;
	private Renderer		renderer;

	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	void Update () {
		renderer.material.SetColor("_Color", new Color(0.6f, 0.6f, transform.position.z / 2 - 5));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			partInstance = (ParticleSystem)Instantiate(deathParticles);
			partInstance.gameObject.transform.position = gameObject.transform.position;
			partInstance.Play();
			GameObject.Destroy(partInstance.gameObject, partInstance.duration);
		}
		GameObject.Destroy(gameObject);
	}
}
