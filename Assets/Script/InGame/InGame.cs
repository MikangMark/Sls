using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : Singleton<InGame>
{
    public Character.CharInfo charInfo;
    public bool openDeckView;
    public int currentFloor = 0;

    public int rewardGold;
    public int rewardCardGroup;
    public List<List<CardInfo>> rewardCards;
    public List<int> rewardCardsIndex = new List<int>();

    public GameObject disCardWarringWiew;
    public ShopScript shopscript;

    public GameObject UnknownDisCardWarringWiew;

    public string playerPrefsKey = "SavedPlayerStat";
    public string saveMapFloorKey = "SavedClearMap";

    public bool monsterSkillReading = false;

    public RelicExcelDataLoader relicExcelDataLoader;
    public List<Relic> treasureRewardList;
    public GameObject relicInfoUi;
    public List<GameObject> haveRelicList;
    public List<int> relicIndexList;

    public string lastRoomTag;

    delegate void RelicDelegate();
    RelicDelegate[] relicDelegates = new RelicDelegate[10];
    private void Awake()
    {
        Init();
        openDeckView = false;
        charInfo = Character.Instance.ironclead;
        //charInfo = Character.Instance.ironclead;
        switch (PlayerPrefs.GetInt("CharType")) //픽창에서선텍한 캐릭
        {
            case 0:
                charInfo = Character.Instance.ironclead;//아이언클래드
                break;
            case 1:
                charInfo = Character.Instance.silence;//사일런스
                break;
            case 2:
                charInfo = Character.Instance.defact;//디펙트
                break;
            case 3:
                charInfo = Character.Instance.wacher;//와쳐
                break;
            default:
                charInfo = Character.Instance.ironclead;//아이언클래드
                break;
        }
        SavePlayerData();
    }
    private void Start()
    {
        rewardCardGroup = CreateSeed.Instance.RandNum(1, 3);
        LoadPlayerData();
        haveRelicList = new List<GameObject>();
        relicIndexList = new List<int>();
        
        relicDelegates[0] = AncientTeaSet;
        relicDelegates[1] = PreservedInsect;
        relicDelegates[2] = BagOfMarbles;
        relicDelegates[3] = Vajra;
        relicDelegates[4] = Anchor;
        relicDelegates[5] = Strawberry;
        relicDelegates[6] = CeramicFish;
        relicDelegates[7] = SmilingMask;
        relicDelegates[8] = CentennialPuzzle;
        relicDelegates[9] = ArtOfWar;
        //SetReward();
    }

    public void ClearRelicReward()
    {
        treasureRewardList.Clear();
    }
    public void SetTreasureRelic()
    {
        for(int i = 0; i < 3; i++)
        {
            relicIndexList.Add(CreateSeed.Instance.RandNum(0, relicExcelDataLoader.relicInfo.Count - 1));
            treasureRewardList.Add(relicExcelDataLoader.relicInfo[relicIndexList[i]]);
        }
    }

    public void SetReward()
    {
        rewardGold = CreateSeed.Instance.RandNum(50, 100);
        
        rewardCards = new List<List<CardInfo>>();
        for (int i = 0; i < rewardCardGroup; i++)
        {
            rewardCards.Add(new List<CardInfo>());
            for (int j = 0; j < 3; j++)
            {
                int cardnumRand = CreateSeed.Instance.RandNum(0, Deck.Instance.cardList.Count - 1);
                rewardCardsIndex.Add(cardnumRand);
                if (cardnumRand == 4)//슬라임카드
                {
                    rewardCardsIndex.RemoveAt(rewardCardsIndex.Count - 1);
                    j--;
                }
                else
                {
                    rewardCards[i].Add(Deck.Instance.cardList[cardnumRand]);
                }
            }
        }
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(charInfo);
        PlayerPrefs.SetString(playerPrefsKey, json);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt(saveMapFloorKey, currentFloor);
    }

    public void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            string json = PlayerPrefs.GetString(playerPrefsKey);
            charInfo = JsonUtility.FromJson<Character.CharInfo>(json);
        }
        currentFloor = PlayerPrefs.GetInt(saveMapFloorKey);
    }

    public void RelicActive()
    {
        for(int i = 0; i < haveRelicList.Count; i++)
        {
            SearchToIndexRelic(haveRelicList[i]);
        }
    }
    public void SearchToIndexRelic(GameObject _relic)
    {
        for(int i = 0; i < haveRelicList.Count; i++)
        {
            
        }
    }

    #region 유물함수

    public void AncientTeaSet()//고대의 차 세트
    {

    }

    public void PreservedInsect()//곤충박제
    {

    }

    public void BagOfMarbles()//구슬 주머니
    {

    }

    public void Vajra()//금강저
    {

    }

    public void Anchor()//닻
    {

    }

    public void Strawberry()//딸기
    {

    }

    public void CeramicFish()//물고기 도자기
    {

    }

    public void SmilingMask()//미소짓는 가면
    {

    }

    public void CentennialPuzzle()//백년퍼즐
    {

    }

    public void ArtOfWar()//병법서
    {

    }
    #endregion
}
