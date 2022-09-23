using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class cp_PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
        List<GameObject> gameObjectsList = new List<GameObject>();

        [SerializeField]
        GameObject m_PlacedPrefab1;
        [SerializeField]
        GameObject m_PlacedPrefab2;

        private float paintScore = 0f;
        [SerializeField]
        Text scoreText;

        //[SerializeField]
        //ARPlaneManager arPlaneManager;
        
        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            //PlaneDetectionMode currentDetectionMode = new ARPlaneManager().detectionMode;
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;
                Quaternion Fixminus = Quaternion.Euler(-90, 0, 0);
                int paintSwitch = Random.Range(0, 2);

                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    //if (paintScore > 100)
                    //{
                    //    m_PlacedPrefab.GetComponent<Renderer>().material.color = Color.blue;
                    //}
                    if (hit.collider.CompareTag("paintPlane"))
                    {

                    }
                    else
                    {
                        paintScore += 1f;
                        switch (paintSwitch)
                        {
                            case 0:
                                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation * Fixminus);
                                break;
                            case 1:
                                spawnedObject = Instantiate(m_PlacedPrefab1, hitPose.position, hitPose.rotation * Fixminus);
                                break;
                            case 2:
                                spawnedObject = Instantiate(m_PlacedPrefab2, hitPose.position, hitPose.rotation * Fixminus);
                                break;
                            default:
                                break;
                        }
                        if (spawnedObject.transform.localEulerAngles.z > 0f)//垂直の時
                        {
                            //spawnedObject.GetComponent<Renderer>().material.color = Color.blue;
                            spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Blue");
                        }
                        else if(spawnedObject.transform.localEulerAngles.x > 0f)
                        {
                            //spawnedObject.GetComponent<Renderer>().material.color = Color.red;
                            spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Red");
                        }
                    }
                }

                scoreText.text = spawnedObject.transform.localEulerAngles.ToString();

                //if (spawnedObject == null)
                //{
                //    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                //}
                //else
                //{
                //    spawnedObject.transform.position = hitPose.position;
                //}
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
