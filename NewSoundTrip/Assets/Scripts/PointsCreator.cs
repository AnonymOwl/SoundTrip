using UnityEngine;
using System.Collections;

public class PointsCreator : MonoBehaviour {

	public int			distance = 1;
	public GameObject	parent;
	private float		cosAngle;
	private float		sinAngle;

	void Start () {
		cosAngle = Mathf.Cos(Mathf.Deg2Rad * 45);
		sinAngle = Mathf.Sin(Mathf.Deg2Rad * 45);

		Transform p0 = parent.transform.FindChild("Point0");
		p0.position = new Vector3(0, distance, 0);
		Transform p1 = parent.transform.FindChild("Point1");
		p1.position = new Vector3(cosAngle * distance, sinAngle * distance, 0);
		Transform p2 = parent.transform.FindChild("Point2");
		p2.position = new Vector3(distance, 0, 0);
		Transform p3 = parent.transform.FindChild("Point3");
		p3.position = new Vector3(cosAngle * distance, -sinAngle * distance, 0);
		Transform p4 = parent.transform.FindChild("Point4");
		p4.position = new Vector3(0, -distance, 0);
		Transform p5 = parent.transform.FindChild("Point5");
		p5.position = new Vector3(-cosAngle * distance, -sinAngle * distance, 0);
		Transform p6 = parent.transform.FindChild("Point6");
		p6.position = new Vector3(-distance, 0, 0);
		Transform p7 = parent.transform.FindChild("Point7");
		p7.position = new Vector3(-cosAngle * distance, sinAngle * distance, 0);

		GameObject.Find("Tunnel").transform.localScale = new Vector3(distance * 2.5f, distance * 2.5f, 1);
	}
}
