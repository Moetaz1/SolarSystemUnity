using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 1000f;
    GameObject[] celestials;

    [SerializeField]
    bool IsElipticalOrbit = false;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");

        SetInitialVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();
    }

    void SetInitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    if (IsElipticalOrbit)
                    {
                        // Ensure that smaller celestial object orbits around the larger one
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f)))) / Mathf.Sqrt(m1);
                    }
                    else
                    {
                        // Ensure that smaller celestial object orbits around the larger one
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r) / Mathf.Sqrt(m1);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    // Calculate gravitational force only for the lighter object
                    Vector3 forceDirection = (b.transform.position - a.transform.position).normalized;
                    Vector3 force = forceDirection * (G * (m1 * m2) / (r * r));

                    // Apply force to the lighter object
                    if (m1 < m2)
                    {
                        a.GetComponent<Rigidbody>().AddForce(force);
                    }
                }
            }
        }
    }
}
