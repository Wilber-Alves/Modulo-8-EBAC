using UnityEngine;
using UnityEngine.UI;
public class ContainerView : MonoBehaviour
{

    public GameObject _containerView;
    
    public void OnClickContainerView()
    { 
    
       _containerView.SetActive(!_containerView.activeSelf);

    }
}
