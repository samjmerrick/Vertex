using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float duration = 2f;
    public float speed = 20f;
    public float magnitude = 2f;
    public AnimationCurve damper = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(0.9f, .33f, -2f, -2f), new Keyframe(1f, 0f, -5.65f, -5.65f));
    public bool testPosition = false;
    public bool testRotation = false;
    public bool testProjection = false;

    private Transform ship;

    Vector3 originalPos;
    Quaternion originalRot;

    private void OnEnable()
    {
        Enemy.Death += StartShake;
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    private void OnDisable()
    {
        Enemy.Death -= StartShake;
    }

    void StartShake(string name, Vector3 pos)
    {
        if (ship == null)
            ship = GameObject.FindGameObjectWithTag("Player").transform;

        float diff = Vector2.Distance(ship.position, pos);

        StartCoroutine(ShakeRotation(transform, originalRot, duration, speed, magnitude / diff, damper));
    }


    void Update()
    {
        if (testPosition)
        {
            testPosition = false;
            StopAllCoroutines();
            StartCoroutine(ShakePosition(transform, originalPos, duration, speed, magnitude, damper));
        }
        else if (testRotation)
        {
            testRotation = false;
            StopAllCoroutines();
            StartCoroutine(ShakeRotation(transform, originalRot, duration, speed, magnitude, damper));
        }
        else if (testProjection)
        {
            testProjection = false;
            StopAllCoroutines();
            StartCoroutine(ShakeCameraProjection(Camera.main, duration, speed, magnitude, damper));
        }
    }


    IEnumerator ShakePosition(Transform transform, Vector3 originalPosition, float duration, float speed, float magnitude, AnimationCurve damper = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float damperedMag = (damper != null) ? (damper.Evaluate(elapsed / duration) * magnitude) : magnitude;
            float x = (Mathf.PerlinNoise(Time.time * speed, 0f) * damperedMag) - (damperedMag / 2f);
            float y = (Mathf.PerlinNoise(0f, Time.time * speed) * damperedMag) - (damperedMag / 2f);
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            yield return null;
        }
        transform.localPosition = originalPosition;
    }


    IEnumerator ShakeRotation(Transform transform, Quaternion originalRotation, float duration, float speed, float magnitude, AnimationCurve damper = null)
    {
        Vector3 originalEuler = originalRotation.eulerAngles;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float damperedMag = (damper != null) ? (damper.Evaluate(elapsed / duration) * magnitude) : magnitude;
            float x = (Mathf.PerlinNoise(Time.time * speed, 0f) * damperedMag) - (damperedMag / 2f);
            float y = (Mathf.PerlinNoise(0f, Time.time * speed) * damperedMag) - (damperedMag / 2f);
            float z = (Mathf.PerlinNoise(0.5f, Time.time * speed * 0.5f) * damperedMag) - (damperedMag / 2f);
            transform.localRotation = Quaternion.Euler(new Vector3(originalEuler.x + x, originalEuler.y + y, originalEuler.z + z));
            yield return null;
        }
        transform.localRotation = originalRotation;
    }


    IEnumerator ShakeCameraProjection(Camera camera, float duration, float speed, float magnitude, AnimationCurve damper = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float damperedMag = (damper != null) ? (damper.Evaluate(elapsed / duration) * magnitude) : magnitude;
            float x = (Mathf.PerlinNoise(Time.time * speed, 0f) * damperedMag) - (damperedMag / 2f);
            float y = (Mathf.PerlinNoise(0f, Time.time * speed) * damperedMag) - (damperedMag / 2f);
            // offset camera obliqueness - http://answers.unity3d.com/questions/774164/is-it-possible-to-shake-the-screen-rather-than-sha.html
            float frustrumHeight = 2 * camera.nearClipPlane * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float frustrumWidth = frustrumHeight * camera.aspect;
            Matrix4x4 mat = camera.projectionMatrix;
            mat[0, 2] = 2 * x / frustrumWidth;
            mat[1, 2] = 2 * y / frustrumHeight;
            camera.projectionMatrix = mat;
            yield return null;
        }
        camera.ResetProjectionMatrix();
    }

}

