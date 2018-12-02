using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorGridExplosion : MonoBehaviour {

    public float m_ForceScale = 1;
    public float m_Radius = 1;
    public float duration = 0.4f;

    private VectorGrid m_VectorGrid;
    private Color m_Color;

	IEnumerator Start ()
    {
        m_VectorGrid = FindObjectOfType<VectorGrid>();
        m_Color = GetComponent<ParticleSystem>().main.startColor.color;

        yield return new WaitForSeconds(duration);
        enabled = false;
    }

	void Update ()
    {
        m_VectorGrid.AddGridForce(this.transform.position, m_ForceScale, m_Radius, m_Color, true);
    }
}
