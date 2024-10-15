using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text Room_name_text;
    RoomInfo info;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        Room_name_text.text = _info.Name;
    }
    public void OnClick()
    {
        Photon_launcher.Instance.Joinroom(info);
    }
}
