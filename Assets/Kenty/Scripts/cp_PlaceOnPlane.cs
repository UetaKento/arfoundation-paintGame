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

        private string m_Color;

        public string Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        private string m_Shape;

        public string Shape
        {
            get { return m_Shape; }
            set { m_Shape = value; }
        }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
            m_Color = "Blue";
            m_Shape = "Circle";
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

        private string CombineColorShape(string color, string shape)
        {
            if (color == "Red" || shape == "Circle")
            {
                return "RedCircle";
            }else if (color == "Red" || shape == "Triangle")
            {
                return "RedTriangle";
            }else if (color == "Red" || shape == "Square")
            {
                return "RedSquare";
            }else if (color == "Blue" || shape == "Circle")
            {
                return "BlueCircle";
            }
            else if (color == "Blue" || shape == "Triangle")
            {
                return "BlueTriangle";
            }
            else if (color == "Blue" || shape == "Square")
            {
                return "BlueSquare";
            }else if (color == "Yellow" || shape == "Circle")
            {
                return "YellowCircle";
            }
            else if (color == "Yellow" || shape == "Triangle")
            {
                return "YellowTriangle";
            }
            else
            {
                return "YellowSquare";
            }

        }

        //色(Red、Blue、Yellow)と形(丸、三角、四角)はボタンで変える
        //丸はRedと相性がよく、三角はBlueと相性がよく、四角はYellowと相性がよい
        //
        void Update()
        {
            //PlaneDetectionMode currentDetectionMode = new ARPlaneManager().detectionMode;
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;
                Quaternion Fixminus = Quaternion.Euler(-90, 0, 0);
                int paintSwitch = Random.Range(0, 2);

                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.CompareTag("paintPlane"))
                    {

                    }
                    else
                    {
                        paintScore += 1f;
                        switch (CombineColorShape(m_Color, m_Shape))
                        {
                            case "RedCircle":
                                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Red");
                                break;
                            case "RedTriangle":
                                spawnedObject = Instantiate(m_PlacedPrefab1, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Red");
                                break;
                            case "RedSquare":
                                spawnedObject = Instantiate(m_PlacedPrefab2, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Red");
                                break;
                            case "BlueCircle":
                                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Blue");
                                break;
                            case "BlueTriangle":
                                spawnedObject = Instantiate(m_PlacedPrefab1, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Blue");
                                break;
                            case "BlueSquare":
                                spawnedObject = Instantiate(m_PlacedPrefab2, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Blue");
                                break;
                            case "YellowCircle":
                                spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Yellow");
                                break;
                            case "YellowTriangle":
                                spawnedObject = Instantiate(m_PlacedPrefab1, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Yellow");
                                break;
                            case "YellowSquare":
                                spawnedObject = Instantiate(m_PlacedPrefab2, hitPose.position, hitPose.rotation * Fixminus);
                                spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Yellow");
                                break;
                            default:
                                break;
                        }
                        //switch (paintSwitch)
                        //{
                        //    case 0:
                        //        spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation * Fixminus);
                        //        break;
                        //    case 1:
                        //        spawnedObject = Instantiate(m_PlacedPrefab1, hitPose.position, hitPose.rotation * Fixminus);
                        //        break;
                        //    case 2:
                        //        spawnedObject = Instantiate(m_PlacedPrefab2, hitPose.position, hitPose.rotation * Fixminus);
                        //        break;
                        //    default:
                        //        break;
                        //}

                        //if (spawnedObject.transform.localEulerAngles.z > 0f)//垂直の時
                        //{
                        //    spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Blue");
                        //}
                        //else if(spawnedObject.transform.localEulerAngles.x > 0f)
                        //{
                        //    spawnedObject.GetComponent<Renderer>().material = Resources.Load<Material>("R_Materials/Red");
                        //}
                    }
                }

                scoreText.text = paintScore.ToString();
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
