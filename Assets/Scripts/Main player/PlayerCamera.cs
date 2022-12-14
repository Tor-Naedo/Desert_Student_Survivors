using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] float speed = 10f;

    public static Player player;
    public static Volume volume;

    public static Vignette vignette;

    public static ChromaticAberration chromaticAberration;

    public static Bloom bloom;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        volume = GetComponent<Volume>();

        volume.profile.TryGet(out vignette);

        volume.profile.TryGet(out chromaticAberration);

        volume.profile.TryGet(out bloom);
    }

    public static void BloomOnOff(bool value)
    {
        bloom.active = value;
    }

    public static void ChromaticAberrationOnOff(bool value)
    {
        chromaticAberration.active = value;
    }

    public static void vignetteOnOff(bool value)
    {
        vignette.active = value;
    }

    private void Update()
    {
        vignette.intensity.Override(1 - player.GetHPRatio());

        if (target == null)
        {
            return;
        }

        float playerX = target.transform.position.x;
        float playerY = target.transform.position.y;
        float cameraZ = transform.position.z;

        var transformPosition = new Vector3(playerX, playerY, cameraZ);
        transform.position = Vector3.MoveTowards(transform.position, transformPosition, speed * Time.unscaledDeltaTime);
    }
}
