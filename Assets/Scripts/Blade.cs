using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;
    public Vector3 direction { get; private set; }

    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;

    private bool slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        #region Android
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                StartSlicing();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                StopSlicing();
            }
            else
            {
                ContinueSlicing();
            }
        }
        #endregion

        #region PC
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
        #endregion
    }

    private void StartSlicing()
    {
        Vector3 newPositon = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPositon.z = 0f;

        transform.position = newPositon;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //TODO: this
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }
}
