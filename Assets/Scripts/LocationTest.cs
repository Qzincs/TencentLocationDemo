using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TencentLocation;
using Unity.VisualScripting;

//��TencentLocationService������ͬһ�����ϵĽű�ʵ��ILocationListener�ӿں����ͨ���ص���ȡ��λ���
public class LocationTest : MonoBehaviour, ILocationListener 
{
    public TextMeshProUGUI infoText;
    public TencentLocationService locationService;

    void Awake()
    {
        // ͬ����˽Э��
        TencentLocationService.SetAgreePrivacy(true);
    }

    /// <summary>
    /// ��ʼ��λ
    /// </summary>
    public void StartLocation()
    {
        if(locationService.status == TencentLocationServiceStatus.Ready || locationService.status == TencentLocationServiceStatus.Stopped)
        {
            locationService.Start();
        }
    }

    void Update()
    {
        switch (locationService.status)
        {
            case TencentLocationServiceStatus.Initializing:
                infoText.text = "Location Service Initializing";
                break;
            case TencentLocationServiceStatus.Ready:
                infoText.text = "Location Service Ready";
                break;
            case TencentLocationServiceStatus.Failed:
                infoText.text = $"Location Service Failed. ErrorCode: {locationService.errorCode}";
                break;
        }
    }

    /// <summary>
    /// ֹͣ��λ
    /// </summary>
    public void StopLocation()
    {
        locationService.Stop();
        infoText.text = "Location Service Stopped";
    }

    /// <summary>
    /// ��λ��Ϣ���»ص�
    /// </summary>
    /// <param name="locInfo">��λ��Ϣ</param>
    public void OnLocationUpdate(string locInfo)
    {
        // ������λ��Ϣ
        TencentLocationInfo info = Util.ParseLocationInfo(locInfo);
        infoText.text = $"{info.timestamp}: {info.latitude}, {info.longitude}, {info.altitude}";
    }
}
