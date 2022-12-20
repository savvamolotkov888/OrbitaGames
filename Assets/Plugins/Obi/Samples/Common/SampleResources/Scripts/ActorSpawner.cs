using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ActorSpawner : MonoBehaviour {

	public ObiActor template;

	public int basePhase = 2;
	public int maxInstances = 32;
	public float spawnDelay = 0.3f;

	private int phase = 0;
	private int instances = 0;
	private float timeFromLastSpawn = 0;
int t =0;
	// Update is called once per frame
	void Update () {

		timeFromLastSpawn += Time.deltaTime;

		if (Input.GetMouseButtonDown(0) && instances < maxInstances && timeFromLastSpawn > spawnDelay)
		{
			GameObject go = Instantiate(template.gameObject,new Vector3(t,0,0),Quaternion.identity);
            go.transform.SetParent(transform.parent);
			t += 1;
       //     go.GetComponent<ObiActor>().SetPhase(basePhase + phase);

			phase++;
			instances++;
			timeFromLastSpawn = 0;
		}
	}
}
