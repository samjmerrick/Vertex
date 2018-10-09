using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorGridScroll : MonoBehaviour {

    public Vector2 m_ScrollSpeed;
    VectorGrid m_VectorGrid;

	// Use this for initialization
	void Start () {
        m_VectorGrid = GetComponent<VectorGrid>();
	}
	
	// Update is called once per frame
	void Update () {
        m_VectorGrid.Scroll(m_ScrollSpeed * Time.deltaTime);
    }
}
