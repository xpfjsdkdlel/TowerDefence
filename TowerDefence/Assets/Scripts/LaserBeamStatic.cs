using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamStatic : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject beamLineRendererPrefab; // 라인 렌더러가 있는 프리팹
    public GameObject beamStartPrefab; // 빔의 시작 부분에 놓이는 프리팹
    public GameObject beamEndPrefab; // 빔의 끝부분에 놓이는  프리팹

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    [Header("Beam Options")]
    public bool alwaysOn = true; // 스크립트가 로드될 때 빔을 발생시키려면 이 옵션을 선택합니다.
    public bool beamCollides = true; // 충돌시 빔 정지
    public float beamLength = 100; // 인게임 빔 길이
    public float beamEndOffset = 0f; // 레이캐스트 hit point에서 end effect가 위치하는 거리
    public float textureScrollSpeed = 0f; // 빔을 따라 텍스쳐가 스크롤되는 속도 (음수 or 양수)
    public float textureLengthScale = 1f;   // 텍스쳐의 세로 길이
                                            // 예: 텍스처가 높이가 200픽셀이고 길이가 600인 경우 이를 3으로 설정합니다.

    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (alwaysOn) // 오브젝트가 Enable되면 빔을 생성
            SpawnBeam();
    }

    private void OnDisable() // 오브젝트가 Disable되면 빔을 삭제
    {
        RemoveBeam();
    }

    void FixedUpdate()
    {
        if (beam) // 빔의 업데이트
        {
            Vector3 end;
            line.SetPosition(0, transform.position);
            // 적을 하도록 적용
            int layerMask = 1 << LayerMask.NameToLayer("Water");
            RaycastHit hit;
            if (beamCollides && Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity,layerMask)) //Checks for collision
                end = hit.point - (transform.forward * beamEndOffset);
            else
                end = transform.position + (transform.forward * beamLength);

            line.SetPosition(1, end);

            if (beamStart)
            {
                beamStart.transform.position = transform.position;
                beamStart.transform.LookAt(end);
            }
            if (beamEnd)
            {
                beamEnd.transform.position = end;
                beamEnd.transform.LookAt(beamStart.transform.position);
            }

            float distance = Vector3.Distance(transform.position, end);
            line.material.mainTextureScale = new Vector2(distance / textureLengthScale, 1); // 신축성이 없어보이는 텍스쳐의 스케일을 잡아줌
            line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0); // 0으로 설정되지 않은 경우 빔을 따라 텍스처가 스크롤
            }
    }

    public void SpawnBeam() // 이 함수는 LineRenderer와 함께 프리팹을 생성합니다.
        {
        if (beamLineRendererPrefab)
        {
            if (beamStartPrefab)
                beamStart = Instantiate(beamStartPrefab);
            if (beamEndPrefab)
                beamEnd = Instantiate(beamEndPrefab);
            beam = Instantiate(beamLineRendererPrefab);
            beam.transform.position = transform.position;
            beam.transform.parent = transform;
            beam.transform.rotation = transform.rotation;
            line = beam.GetComponent<LineRenderer>();
            line.useWorldSpace = true;
            #if UNITY_5_5_OR_NEWER
			line.positionCount = 2;
			#else
			line.SetVertexCount(2); 
			#endif
        }
        else
            print("Add a prefab with a line renderer to the MagicBeamStatic script on " + gameObject.name + "!");
    }

    public void RemoveBeam() // LineRenderer가 있는 프리팹을 제거
        {
        if (beam)
            Destroy(beam);
        if (beamStart)
            Destroy(beamStart);
        if (beamEnd)
            Destroy(beamEnd);
    }
}