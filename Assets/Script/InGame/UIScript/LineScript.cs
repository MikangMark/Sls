using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
	public List<GameObject> target;//연결할 오브젝트 
	public List<LineRenderer> lineObj;//라인렌더러 오브젝트
	public LineRenderer linePfab;
	public Texture2D tile;
	private void Start()
	{
		StartCoroutine(CreateLine());
	}
    private void Update()
    {
		for(int i = 0; i < lineObj.Count; i++)
        {
			lineObj[i].SetPosition(0, transform.position);
			lineObj[i].SetPosition(1, target[i].transform.position);
		}
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
}
