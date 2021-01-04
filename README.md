# [URG] Unity-Rhythm-Game

## 개요
이 리포지토리는 음악과 함께하는 건반 형식의 리듬 게임 템플릿을 제공합니다. 
또한, 프로젝트는 유니티 버전 `2020.2.0f1`로 개발되었습니다. 
빌트인 렌더링 파이프라인을 사용하고 있습니다.
  
저는 `DJMAX RESPECT V`를 좋아해서 UX 제작에 많이 참고하였습니다. 
`PSP` 때 부터 즐겨왔고, 유니티 개발력 향상을 위해 존중을 담아 모작을 진행해 보는 것이 좋은 경험이 될 거라고 생각했습니다. 
  
이 프로젝트의 기반 프레임워크로 BMS를 선택했습니다. 
우선 이미 만들어져있는 패키지들이 다수 있어, 개발에 용이하기 때문입니다. 
우연히 DJMAX 온라인의 BMS 패키지들을 발견하였고, 개발 테스트 용도로 사용하였습니다.
본 리포지토리에는 해당 콘텐츠들이 포함되어있지 않습니다. 
앞으로 자체적인 포맷을 사용하고, BMS 패키지를 자체 포맷으로 변환할 수 있도록 작업할 예정입니다.

## 저작권
프로젝트 루트 경로의 `resource` 폴더에 위치하는 스프라이트 리소스들 중 psd 파일이 있는 것들은 직접 제작한 에셋들입니다.
노트 이미지, 기어 스프라이트 등은 직접 제작했으므로 사용하셔도 되나, DJMAX의 디자인 UX 자체에 대한 모든 권리는 제작사인 네오위즈에 있습니다.

## 로드맵
### BMS
* BMS 파서
* BMS 에디터
* BMS 자동 생성 도구
* BMS <-> 자체 포맷 컨버터
### 개발
* 뉴 인풋 시스템 도입
* 콘솔 포팅, UI 개선
* 콘텐츠 에셋 번들화

## 사용된 에셋들
* Flash FX Pack - Youtube
* DOTween v2
* More Effective Coroutine
* BMPLoader
* UniRx

## 사용된 패키지
* TextMeshPro

## 참고

- Bitmap Loader : [BMPLoader - Pastebin.com](https://pastebin.com/fykWMpuB)
- BMS Parser python : [dxnoob / bms-parser · GitLab](https://gitlab.com/dxnoob/bms-parser)
- BMS Parser php : [sonoritycomm/bms-parser](https://github.com/sonoritycomm/bms-parser)

- FX : [https://youtu.be/qb8QDbdQ758](https://youtu.be/qb8QDbdQ758)