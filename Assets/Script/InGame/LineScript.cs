using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
	public List<GameObject> target;//������ ������Ʈ 
	public List<LineRenderer> lineObj;//���η����� ������Ʈ
	public LineRenderer linePfab;
	private void Start()
	{
		StartCoroutine(CreateLine());
	}

	IEnumerator CreateLine()
	{
		yield return null;

		if (target.Count <= 0)
		{
			yield break;
		}

		lineObj = new List<LineRenderer>();

		for (int i = 0; i < target.Count; i++)
		{
			LineRenderer line = Instantiate<LineRenderer>(linePfab, transform);
			line.name = "[" + GetComponent<Room>().node.roomName + "]2Line[" + target[i].GetComponent<Room>().node.roomName + "]";
			lineObj.Add(line);

			line.useWorldSpace = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, target[i].transform.position);
		}
	}
	public void AddTarget(GameObject _target)
    {
		target.Add(_target);
    }
}
