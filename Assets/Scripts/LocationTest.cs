using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TencentLocation;
using Unity.VisualScripting;

//与TencentLocationService挂载在同一物体上的脚本实现ILocationListener接口后可以通过回调获取定位结果
public class LocationTest : MonoBehaviour, ILocationListener 
{
    public TextMeshProUGUI infoText;
    public TencentLocationService locationService;

    void Awake()
    {
        // 同意隐私协议
        TencentLocationService.SetAgreePrivacy(true);
    }

    /// <summary>
    /// 开始定位
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
    /// 停止定位
    /// </summary>
    public void StopLocation()
    {
        locationService.Stop();
        infoText.text = "Location Service Stopped";
    }

    /// <summary>
    /// 定位信息更新回调
    /// </summary>
    /// <param name="locInfo">定位信息</param>
    public void OnLocationUpdate(string locInfo)
    {
        // 解析定位信息
        TencentLocationInfo info = Util.ParseLocationInfo(locInfo);
        infoText.text = $"{info.timestamp}: {info.latitude}, {info.longitude}, {info.altitude}";
    }
}
