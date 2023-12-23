using System.Collections;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private Material _cleanMaterial;
    [SerializeField] private float _rubbingDuration = 2f;
    [SerializeField] private GameObject visauls;
    [SerializeField] private GameObject _soapBubbleFXPrefab;
    [SerializeField] private GameObject _bruchFXPrefab;
    [SerializeField] private GameObject _cleanFXPrefab;
    [SerializeField] private AudioClip _scrubSound;


    private Material _originalMaterial;
    private bool _isRubbing = false;
    private GameObject soapBubbleFX;
    private int rubCount = 0;
    private AudioSource _audioSource;
    private bool _isCleaned = false;
    private int hitCount = 0;

    private void Start()
    {
        _originalMaterial = visauls.GetComponent<Renderer>().material;
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = _scrubSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isRubbing && other.CompareTag("Sponge"))
        {
            _isRubbing = true;
            StartCoroutine(RubbingRoutine());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            hitCount++;
            if (hitCount >= 400 && _isCleaned)
            {
                Instantiate(_cleanFXPrefab, transform.position, Quaternion.identity, transform);
                ChangeMaterial(_cleanMaterial);
                hitCount= 0;    
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!_isRubbing && other.CompareTag("Sponge"))
        {
            _isRubbing = false;
        }
    }

    private IEnumerator RubbingRoutine()
    {
        PlaySoapBubbleFX();
        PlayScrubSound();
        yield return new WaitForSeconds(_rubbingDuration);
        StopSoapBubbleFX();
        rubCount++;
        if (rubCount == 3) {
            _isCleaned = true;
            Instantiate(_bruchFXPrefab, transform.position, Quaternion.identity, transform);
            // ChangeMaterial(_cleanMaterial); 
        }       
        _isRubbing = false;
    }

    private void ChangeMaterial(Material newMaterial)
    {
        visauls.GetComponent<Renderer>().material = newMaterial;
    }
    private void PlayScrubSound()
    {
        _audioSource.Play();
    }
    private void PlaySoapBubbleFX()
    {
        soapBubbleFX = Instantiate(_soapBubbleFXPrefab, transform.position, Quaternion.identity,transform);
        soapBubbleFX.transform.Rotate(new Vector3(1, 0, 0), -90f);
    }

    private void StopSoapBubbleFX()
    {
        Destroy(soapBubbleFX);
    }
}
