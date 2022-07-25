using UnityEngine;

public class Graber: MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private LayerMask layerMask;
    //[SerializeField] private GameObject GunTransformer;

    [SerializeField] private float zLimitMax;
    [SerializeField] private float zLimitMin;

    [SerializeField] private float xLimitMax;
    [SerializeField] private float xLimitMin;

    [SerializeField] private GameObject selectedObject;
    [SerializeField] private PositionHelper posHelper;
    [SerializeField] private GameObject featurePlatform;
    [SerializeField] private GunManager gunManager;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    if (hit.collider.gameObject.CompareTag(targetTag))
                    {
                        selectedObject = hit.collider.gameObject;
                        Debug.Log(1);
                        gunManager.CheckAllPlatformsWithIdebtityLevel(selectedObject.GetComponent<Gun>().GetGunLevel());
                        Debug.Log(2);
                        selectedObject.GetComponent<PositionHelper>().GetStartPlatform().GetWhiteBodyOfPlatform().GetComponent<MeshRenderer>().material.color = Color.white;
                        //posHelper.SetStartPlatfrom(gameObject.transform.parent.gameObject);
                        selectedObject.transform.SetParent(null);
                    }
                }
            }
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worlPosition = Camera.main.ScreenToWorldPoint(position);

            float Zpos = worlPosition.z;
            float Xpos = worlPosition.x;

            if (Zpos > zLimitMax)
            {
                Zpos = zLimitMax;
            }
            else if (Zpos < zLimitMin)
            {
                Zpos = zLimitMin;
            }

            selectedObject.transform.position = new Vector3(Xpos, 2, Zpos);

            if (Input.GetMouseButtonUp(0))
            {
                gunManager.SetWhiteStatusForPlatforms();

                posHelper = selectedObject.GetComponent<PositionHelper>();
                featurePlatform = posHelper.GetDownPlatform();

                if (featurePlatform == null)
                {
                    Destroy(selectedObject);
                    selectedObject = null;
                }
                else
                {
                    selectedObject.transform.SetParent(featurePlatform.transform);
                    selectedObject.transform.position = featurePlatform.GetComponent<Platform>().GetSpawnGunPoint().position;
                    selectedObject.GetComponent<Animator>().SetTrigger("Bounce");
                    selectedObject = null;
                }

                GameEvents.OnSaveAction();
            }
        }
    }

    

}