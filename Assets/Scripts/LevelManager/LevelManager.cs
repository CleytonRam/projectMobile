using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;  

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;
    public float timeBetweenPieces = .3f;

 
    private GameObject _currentLevel;

    [SerializeField]private List<LevelPieceBase> _spawnedPieces =   new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;


    private void Awake()
    {
     
       
        CreateLevelPieces();
    }
    private void SpawnNextLevel() 
    {
        if (_currentLevel != null) 
        {
            Destroy(_currentLevel);
       
        }
        _currentLevel = Instantiate(levels[Random.Range(0, levels.Count)], container);
       
        _currentLevel.transform.localPosition = Vector3.zero;
    }


    #region
    private void CreateLevelPieces() 
    {
        
        CleanSpawnedPieces();
        _currSetup = levelPieceBasedSetups[Random.Range(0, levelPieceBasedSetups.Count)];


       


        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesStart);
            
        }
       

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
           // yield return new WaitForSeconds(timeBetweenPieces);
        }
        //StartCoroutine(CreateLevelPiecesCourotine());
        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd);

        }

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
        StartCoroutine(ScalePieceByTime());
    }

    IEnumerator ScalePieceByTime() 
    { 
        foreach(var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
            
        }
        yield return null;
        for (int i = 0; i < _spawnedPieces.Count; i++)
        {
           _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
           yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
        CoinsAnimationManager.Instance.StartAnimations();
    }
    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        if (list != null && list.Count > 0)
        {
            var piece = list[Random.Range(0, list.Count)];
            var spawnedPiece = Instantiate(piece, container);

            if (_spawnedPieces.Count > 0)
            {
                var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

                spawnedPiece.transform.position = lastPiece.endPiece.position;
            }
            else
            {
                spawnedPiece.transform.localPosition = Vector3.zero;
            }

            foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
            {
                p.ChangePiece(ArtManager.Instance.GetSetUpByType(_currSetup.artType).gameObject);

            }
            _spawnedPieces.Add(spawnedPiece);
        }
        else
        {
            Debug.LogWarning("Piece List is empty or null");
        }
    }

    private void CleanSpawnedPieces() 
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--) 
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCourotine() 
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            CreateLevelPieces();
        }
    }
    public void RandomNewMap()
    {
        CreateLevelPieces();
    }
}
