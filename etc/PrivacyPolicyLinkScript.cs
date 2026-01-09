using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyLinkScript : MonoBehaviour {
    public void PrivacyPolicyLinkRun() {
        Application.OpenURL("https://origiaworks.com/application-privacypolicy");
    }
}
