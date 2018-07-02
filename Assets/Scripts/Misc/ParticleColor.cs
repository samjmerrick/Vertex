using UnityEngine;

public class ParticleColor : MonoBehaviour {

    public Color color;

	void Start () {

        var trails = GetComponent<ParticleSystem>().trails;

        trails.colorOverLifetime = ChangeColor(color, Random.Range(0.1f, -0.1f));
	}

    Color ChangeColor(Color colorRef, float change)
    {
        return new Color(colorRef.r + change,
                         colorRef.g + change,
                         colorRef.b + change);
    }

}
